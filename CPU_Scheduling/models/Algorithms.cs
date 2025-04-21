namespace CPU_SCHEDULING.Models {

    using System.Diagnostics;

    public class Algorithms
    {
    
        /* Shortest Job First (SJF) Scheduling Algorithm
            - Non-preemptive: once a process starts executing, it runs to completion.
            - Selects the process with the smallest burst time from the ready queue.
        */
        public static void ShortestJobFirst(List<Process> processes)
        { 
            Console.WriteLine("\n============SHORTEST JOB FIRST ============");

            //Test 
            LogProcessSchedule(processes);
            
            //Check for empty list
            if (processes.Count > 0){

                Stopwatch totalTime = new();

                //Measure time taken to run the algorithm
                totalTime.Start();

                
                int currentTime = 0;
                List<Process> processQueue = new();
                List<Process> remainingProcesses = processes.OrderBy(p => p.ArrivalTime).ToList();
                Process? nextProcess = remainingProcesses.First(); //first shouldnt be null, but may be null after the first

                //Initiate processes
                while(remainingProcesses.Count > 0){

                    //Find processes that have arrived based on their arrival time and the current time
                    List<Process> arrivedProcess = remainingProcesses.Where(p=> p.ArrivalTime <= currentTime).ToList();

                    //If no process has arrived, increment time counter forward
                    if(arrivedProcess.Count == 0){
                        currentTime++;
                    }

                    //If process has arrived, find the job with the shortest BT
                    nextProcess = arrivedProcess.OrderByDescending(p => p.BurstTime).First();

                    if(nextProcess == null){ 
                        break;
                    }

                    //Calculate its completion time
                    nextProcess.CompletionTime = currentTime + nextProcess.BurstTime;                    currentTime += nextProcess.CompletionTime;

                    //update the list
                    remainingProcesses.Remove(nextProcess);

                    totalTime.Stop();
            
                    
                }  
                
                /*LOG RESULTS*/
                LogProcessSchedule(processQueue);
                LogProcessStats(processQueue, totalTime);
            } 
            
            else {
                Console.WriteLine("No processes in list");
            }
        }

        /* Shortest Remaining Time First (SRTF) Scheduling Algorithm
            - Preemptive: allows a process to be interrupted and moved to the ready queue if a new process arrives with a shorter burst time.
            - Selects the process with the smallest remaining time from the ready queue.  
        */
        public static void ShortestRemainingTimeFirst(List<Process> processes) {

        Console.WriteLine("\n============SHORTEST REMAINING TIME FIRST ============");

            Stopwatch totalTime = new();
            totalTime.Start();

            //Check list to ensure not empty/null
            if(processes.Count > 0){

                int currentTime = 0;

                //Jobs that need to run
                List<Process> remainingProcesses = processes
                    .Select(p => 
                    {
                        p.RemainingTime = p.BurstTime; // Add & initilize remaining time to each process (starts as the job time)
                        return p;
                    })
                    .ToList();

                List<Process> completedProcesses = new();
                Process activeProcess; 


                // Console.WriteLine("remaining processes post RS calc "); 
                // LogProcessSchedule(processes);


                //Iterate through the remaining processes to initate jobs
                while(remainingProcesses.Count > 0){

                    //Find the processes that have arrived sorted by AT
                    List<Process> arrivedProcess = remainingProcesses
                        .Where(p => p.ArrivalTime <= currentTime)
                        .OrderBy(p => p.ArrivalTime)
                        .ToList();
        
                    //Start if a process is ready
                    if(arrivedProcess.Count > 0){

                        //Set the active process to the job that is available AND with shortest remaining time left
                        activeProcess = arrivedProcess.OrderBy(p => p.RemainingTime).First();

                        // Console.WriteLine("\nACTIVE PROCESS " + activeProcess.Id);
                
                        //Move time forward
                        currentTime++;

                        // Console.WriteLine("\ncurrent time " + currentTime); 

                        activeProcess.RemainingTime --; 

                        // Console.WriteLine("active process RT " + activeProcess.RemainingTime);

                        if(activeProcess.RemainingTime == 0) {
                            activeProcess.CompletionTime = currentTime;
                            completedProcesses.Add(activeProcess);
                            remainingProcesses.Remove(activeProcess);
                        }
                    }
                }
                 LogProcessSchedule(completedProcesses);
                 LogProcessStats(completedProcesses, totalTime);
            }
             else {
                Console.WriteLine("Process list empty");
             }

    }

        /* Highest Response Ratio Next (HRRN) Scheduling Algorithm
            - Non-preemptive: once a process starts executing, it runs to completion.
            - Selects the process with the largest response ratio from the ready queue.
            - Response Ratio = (Waiting Time + Burst Time) / Burst Time

            Accounts for:
            - Processes with the same arrival times and response rates [ie: selects lowest BT]
            - Idle time (processes only run once arrived [ie: Arrival Time])
            - No processes provided

        */
        public static void HighestResponseRatio(List<Process> processes) {

            Stopwatch totalTime = new();
            totalTime.Start();

            //Ensures list is not empty/null 
            if(processes.Count > 0){

                int currentTime = 0;    
                
                List<Process> processQueue = new List<Process>();
                List<Process> remainingProcesses = processes.OrderBy(p => p.ArrivalTime).ToList();
            
                //Get the first process based on AT
                Process? nextProcess = remainingProcesses.First(); //might be null after the first process arrives 
        
                //Iterate through the remaining processes to initate jobs runnning
                while(remainingProcesses.Count > 0) {

                    //Find processes that have arrived based on AT
                    List<Process> arrivedProcess = remainingProcesses.Where(p => p.ArrivalTime <= currentTime).ToList();
  
                    //If no process arrived, increment time
                    if(arrivedProcess.Count == 0){
                        currentTime++;
                    } else {
                        //Calculate the CT times for arrived processses using the currentTime {used to calc TAT, WT, and RR}
                         foreach(Process p in arrivedProcess){
                            p.CompletionTime = currentTime + p.BurstTime;
                        }

                        /*LOG RESULTS*/
                        //LogResponseRates(arrivedProcess);

                        //Find the next process -- Order the list
                        nextProcess = arrivedProcess
                            .OrderByDescending(p => p.ResponseRatio)    //By Highest response rate
                            .ThenBy(p => p.BurstTime)                   //By Burst Time if AT and RR are the same
                            .FirstOrDefault();

                        if (nextProcess == null){ 
                            break; 
                        }

                        //Move the time pointer forward
                        currentTime += nextProcess.BurstTime;
                
                        processQueue.Add(nextProcess);
                        remainingProcesses.Remove(nextProcess); 
                    }
                    
                }
            
                
                /*LOG RESULTS*/
                //LogProcessSchedule(processQueue);
                totalTime.Stop();
                
                Console.WriteLine("\n============ HIGHEST RESPONSE RATIO ============");
                LogProcessStats(processQueue, totalTime);
            } 
            else {
                Console.WriteLine("No processes provided");
            }
        }

        

        /* ----------------------------------------- HELPERS -----------------------------------------*/

        //Calculation Utilities
        public static double CalculateAvgTurnAroundTime(List<Process> processes) {
            
            if(processes.Count == 0 || processes == null) {
                return 0;
            }
            return Math.Round(processes.Average(process => process.TurnaroundTime), 2);
        }

        public static double CalculateAvgWaitTime(List<Process> processes) {

            if(processes.Count == 0 || processes == null) {
                return 0;
            }

            return Math.Round(processes.Average(process => process.WaitTime), 2);
        }
        

        //Logging Utilities
        static void LogProcessInfo(Process p) {
            Console.WriteLine($"{p.Id} \tAT {p.ArrivalTime} \tBT {p.BurstTime} \tCT {p.CompletionTime} \tTAT {p.TurnaroundTime} \tWT {p.WaitTime}");
        }    
        static void LogResponseRates(List<Process> processes) {

            if(processes.Count > 1) {
                Console.WriteLine("Calculating rates...");
                Console.WriteLine("P \tBT \tCT \tRR");
                foreach(Process p in processes){
                    Console.WriteLine($"{p.Id}  \t{p.BurstTime}  \t{p.CompletionTime}  \t{p.ResponseRatio}");
                }
                
                string selectedProcess = processes
                        .OrderByDescending(p => p.ResponseRatio)    //By Highest response rate
                        .ThenBy(p => p.BurstTime)                   //By Burst Time if AT and RR are the same
                        .First().Id;

                Console.WriteLine($"\n*Highest: {selectedProcess} [*if RR =, then selects smallest BT]");

            } else if(processes.Count == 1) {
                Console.WriteLine($"Next process {processes[0].Id}");
            }
           
        }
        static void LogProcessSchedule(List<Process> processes) {
            Console.WriteLine("\n============ Scheduling Summary ============");
            Console.WriteLine("P \tAT \tBT \tCT \tTAT \tWT");
            foreach(Process p in processes){
                Console.WriteLine($"{p.Id} \t{p.ArrivalTime} \t{p.BurstTime} \t{p.CompletionTime} \t{p.TurnaroundTime} \t{p.WaitTime}");
            }
            Console.WriteLine("__________________________________________________");
            Console.Write("Order: ");
            foreach(Process p in processes){
                Console.Write($"{p.Id} ");
            }
        }
        static void LogProcessStats(List<Process> processes, Stopwatch totalTime) {
            Console.WriteLine("\n\nPerformance Metrics ----------");
            Console.WriteLine($"Total Processes Ran: {processes.Count}");
            Console.WriteLine($"Avg. Turnaround Time: {CalculateAvgTurnAroundTime(processes)}");
            Console.WriteLine($"Avg. Wait Time: {CalculateAvgWaitTime(processes)}");            
            Console.WriteLine($"Total Time: {totalTime.ElapsedMilliseconds} ms");
        }
    }
}