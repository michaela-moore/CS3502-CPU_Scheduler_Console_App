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
            Console.WriteLine("\n= = = = = = = SHORTEST JOB FIRST = = = = = = = ");

            //Check for empty list
            if (processes.Count > 0){

                Stopwatch totalTime = new();
                totalTime.Start();

                int currentTime = 0;

                List<Process> completedProcesses = new();
                List<Process> remainingProcesses = processes.OrderBy(p => p.ArrivalTime).ToList();

                //Initiate processes
                while(remainingProcesses.Count > 0){

                    //Find processes that have arrived
                    List<Process> arrivedProcesses = remainingProcesses.Where(p=> p.ArrivalTime <= currentTime).ToList();

                    // Increment currentTime when no process is ready (to simulate idle CPU time)
                    if(arrivedProcesses.Count == 0){
                        currentTime++;
                    
                    //Start the job
                    } else {

                        //Select process with the shortest run time
                        Process? activeProcess = arrivedProcesses.MinBy(p => p.BurstTime);

                        if (activeProcess != null) {
                            //Execute job
                            activeProcess.FirstStartTime = currentTime;
                            activeProcess.CompletionTime = currentTime + activeProcess.BurstTime;   //SJF non-preemptive (runs till completion)
                            activeProcess.RemainingTime = 0;  //job completed              
                            
                            currentTime += activeProcess.BurstTime;

                            //update queues
                            completedProcesses.Add(activeProcess);
                            remainingProcesses.Remove(activeProcess);
                        }
                        
                    }
    
                }  
                totalTime.Stop();
                
                /*LOG RESULTS*/
                LogProcessSchedule(completedProcesses);
                LogProcessStats(completedProcesses, totalTime);
            } 
            
            else {
                Console.WriteLine("No processes available for scheduling");
            }
        }

        /* Shortest Remaining Time First (SRTF) Scheduling Algorithm
            - Preemptive: allows a process to be interrupted and moved to the ready queue if a new process arrives with a shorter burst time.
            - Selects the process with the smallest remaining time from the ready queue.  
        */
        public static void ShortestRemainingTimeFirst(List<Process> processes) {

        Console.WriteLine("\n= = = = = = = SHORTEST REMAINING TIME FIRST = = = = = = = ");

            //Check list to ensure not empty/null
            if(processes.Count > 0){

                Stopwatch totalTime = new();
                totalTime.Start();
                int currentTime = 0;

                //Jobs that need to run
                List<Process> remainingProcesses = new List<Process>(processes);
                List<Process> completedProcesses = new();
                

                // Console.WriteLine("remaining processes post RS calc "); 
                // LogProcessSchedule(processes);


                //Iterate through the remaining processes to initate jobs
                while(remainingProcesses.Count > 0){

                    //Find the processes that have arrived sorted by AT
                    List<Process> arrivedProcesses = remainingProcesses
                        .Where(p => p.ArrivalTime <= currentTime)
                        .OrderBy(p => p.ArrivalTime)
                        .ToList();  

                    if(arrivedProcesses.Count == 0) {
                      // Increment currentTime when no process is ready (to simulate idle CPU time)
                        currentTime++;  
                    }
                    //Start if a process is ready
                    else {

                        //Set the active process to the remaining job with shortest remaining time left
                        Process? activeProcess = arrivedProcesses.MinBy(p => p.RemainingTime);

                        // Console.WriteLine("\nACTIVE PROCESS " + activeProcess.Id);
                        // Console.WriteLine("\ncurrent time " + currentTime); 

                        if (activeProcess != null){

                            //Set start time for jobs starting for the first time
                            if(activeProcess.FirstStartTime == -1) { 
                                activeProcess.FirstStartTime = currentTime; 
                            }

                            //Move time forward
                            currentTime++;

                            activeProcess.RemainingTime --; //decrement the remaining runtime for the job

                            // Console.WriteLine("active process RT " + activeProcess.RemainingTime);

                            if(activeProcess.IsCompleted) {
                                activeProcess.CompletionTime = currentTime;
                                completedProcesses.Add(activeProcess);
                                remainingProcesses.Remove(activeProcess);
                            }
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

            Console.WriteLine("\n= = = = = = = HIGHEST RESPONSE RATE FIRST = = = = = = = ");

            //Ensures list is not empty/null 
            if(processes.Count > 0){
                
                Stopwatch totalTime = new();
                totalTime.Start();
                int currentTime = 0;    
                
                List<Process> completedProcesses = new List<Process>();
                List<Process> remainingProcesses = processes.OrderBy(p => p.ArrivalTime).ToList();
        
                //Iterate through the remaining processes to initate jobs runnning
                while(remainingProcesses.Count > 0) {

                    //Find processes that have arrived based on AT 
                    List<Process> arrivedProcesses = remainingProcesses.Where(p => p.ArrivalTime <= currentTime).ToList();
  
                    //Move time forward if no process ready (idle)
                    if(arrivedProcesses.Count == 0  || arrivedProcesses == null){
                        currentTime++;
                    
                    //Find process to start
                    } else {
                    
                         //Calculate the CT times for arrived processses using the currentTime {used to calc TAT, WT, and RR}
                         foreach(Process p in arrivedProcesses){
                            p.CompletionTime = currentTime + p.BurstTime;
                        }

                        /*LOG RESULTS*/
                        //LogResponseRates(arrivedProcesses);

                        //Select process based on Highst Response Rate
                        Process activeProcess = arrivedProcesses
                            .OrderByDescending(p => p.ResponseRatio)    
                            .ThenBy(p => p.BurstTime)                   // if AT and RR are == then selects smallest BT
                            .First();                           
                        
                        //capture process' starting time
                        activeProcess.FirstStartTime = currentTime;

                        //Move the time pointer forward so active process can complete
                        currentTime += activeProcess.BurstTime;
                        activeProcess.RemainingTime = 0;
        
                        //Update queues                
                        completedProcesses.Add(activeProcess);
                        remainingProcesses.Remove(activeProcess); 
                    
                    }                   
               
                }
            
                
                /*LOG RESULTS*/
                //LogProcessSchedule(completedProcesses);
                totalTime.Stop();
                
                Console.WriteLine("\n============ HIGHEST RESPONSE RATIO ============");
                LogProcessStats(completedProcesses, totalTime);
            } 
            else {
                Console.WriteLine("No processes provided");
            }
        }

        

        /* ----------------------------------------- HELPERS -----------------------------------------*/

        //Calculations 
        public static double CalculateAvgTurnAroundTime(List<Process> processes) {
            
            if(processes.Count == 0 || processes == null) {
                return 0;
            }
            return Math.Round(processes.Average(process => process.TurnaroundTime), 2);
        }
        public static double CalculateAvgResponseTime(List<Process> processes) {
            
            if(processes.Count == 0 || processes == null) {
                return 0;
            }
            return Math.Round(processes.Average(process => process.ResponseTime), 2);
        }
        public static double CalculateAvgWaitTime(List<Process> processes) {

            if(processes.Count == 0 || processes == null) {
                return 0;
            }

            return Math.Round(processes.Average(process => process.WaitTime), 2);
        }
        

        //Logging Statements
        public static void LogProcessInfo(Process p) {
            Console.WriteLine($"{p.Id} \tAT {p.ArrivalTime} \tBT {p.BurstTime} \tCT {p.CompletionTime} \tTAT {p.TurnaroundTime} \tWT {p.WaitTime}");
        }    
        public static void LogResponseRates(List<Process> processes) {

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
        public static void LogProcessSchedule(List<Process> processes) {
            Console.WriteLine("\n - - - - - - - Timing Summary  - - - - - - -");
            Console.WriteLine("P \tAT \tBT \tCT \tTAT \tWT \tResp. T");
            foreach(Process p in processes){
                Console.WriteLine($"{p.Id} \t{p.ArrivalTime} \t{p.BurstTime} \t{p.CompletionTime} \t{p.TurnaroundTime} \t{p.WaitTime} \t{p.ResponseTime}");
            }
            Console.WriteLine("_______________________________________________________");
            Console.Write("Order: ");
            foreach(Process p in processes){
                Console.Write($"{p.Id} ");
            }
        }
        public static void LogProcessStats(List<Process> processes, Stopwatch totalTime) {
            Console.WriteLine("\n\nPerformance Metrics ----------");
            Console.WriteLine($"Total Processes Ran: {processes.Count}");
            Console.WriteLine($"Avg. Turnaround Time: {CalculateAvgTurnAroundTime(processes)}");
            Console.WriteLine($"Avg. Wait Time: {CalculateAvgWaitTime(processes)}");         
            Console.WriteLine($"Avg. Turnaround Time: {CalculateAvgResponseTime(processes)}");
            Console.WriteLine($"Total Time: {totalTime.ElapsedMilliseconds} ms");
        }
    }
}