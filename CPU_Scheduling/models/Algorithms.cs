namespace CPU_SCHEDULING.Models {

    using System.Diagnostics;

    public class Algorithms
    {
    
        /*
            Shortest Job First (SJF) Scheduling Algorithm
            - Non-preemptive: once a process starts executing, it runs to completion.
            - Selects the process with the smallest burst time from the ready queue.
        */
        public static void ShortestJobFirst(List<Process> processes)
        { 

            Stopwatch totalTime = new();

            //Measure time taken to run the algorithm
            totalTime.Start();

            //Set total number of processes to run
            int numberOfProcesses = processes.Count;

            //sort by burst time (smallest to largest)
            processes.Sort((a, b) => a.BurstTime.CompareTo(b.BurstTime));
        
            Process currentProcess; 

            //TODO : This should just itterate through the list based on BTs, but need some 
            //checking in here incase multiple processes have the same BT, then look at AT (IF available)
            //TODO: Need to account if AT is given 


            //Run --> Calculate completion time for each process in the list
            for (int i = 0; i < numberOfProcesses; i++)
            {
                currentProcess = processes[i];

                //Non-preemptive so jobs run until completion
                if (i == 0)
                {
                    currentProcess.CompletionTime = currentProcess.ArrivalTime + currentProcess.BurstTime;
                }
                //otherwise, set completion time to the previous process's completion time + current process's burst time
                else
                {
                    currentProcess.CompletionTime = processes[i - 1].CompletionTime + currentProcess.BurstTime;
                }
            }

            totalTime.Stop();
            
            LogProcessesDetails(processes);
            LogProcessPerformance(numberOfProcesses, totalTime);
        }

        /*
            Shortest Remaining Time First (SRTF) Scheduling Algorithm
            - Preemptive: allows a process to be interrupted and moved to the ready queue if a new process arrives with a shorter burst time.
            - Selects the process with the smallest remaining time from the ready queue.  
        */
    public static void ShortestRemainingTimeFirst(List<Process> processes) {
            //sort processes by AT (smallest to largest)
            //
    }

        /*
            Highest Response Ratio Next (HRRN) Scheduling Algorithm
            - Non-preemptive: once a process starts executing, it runs to completion.
            - Selects the process with the largest response ratio from the ready queue.
            - Response Ratio = (Waiting Time + Burst Time) / Burst Time

            Accounts for:
            - Processes with the same arrival times and response rates [ie: selects lowest BT]
            - Idle time (processes only run once arrived [ie: Arrival Time])
            - No processes provided

        */
        public static void HighestResponseRatio(List<Process> processes) {

            //Ensures list is not empty/null 
            if(processes.Count > 0){

                Stopwatch totalTime = new();
                totalTime.Start();

                int currentTime = 0;    
                
                List<Process> processQueue = new List<Process>();
                List<Process> remainingProcesses = processes.OrderBy(p => p.ArrivalTime).ToList();
            
                //Get the first process based on AT
                Process nextProcess = remainingProcesses.First();
        
                //Initiate processes ---- 
                while(remainingProcesses.Count > 0) {

                    //Find processes that have arrived based on AT
                    List<Process> arrivedProcess = remainingProcesses.Where(p => p.ArrivalTime <= currentTime).ToList();
  
                    //If no process arrived, increment time
                    if(arrivedProcess.Count == 0){
                        currentTime++;
                    }

                     //Calculate the CT times for arrived processses using the currentTime {used to calc TAT, WT, and RR}
                    foreach(Process p in arrivedProcess){
                        p.CompletionTime = currentTime + p.BurstTime;
                    }

                    LogResponseRates(arrivedProcess);

                    //Find the next process -- Order the list
                    nextProcess = arrivedProcess
                        .OrderByDescending(p => p.ResponseRatio)    //By Highest response rate
                        .ThenBy(p => p.BurstTime)                   //By Burst Time if AT and RR are the same
                        .First();

                    //Move the time pointer forward
                    currentTime += nextProcess.BurstTime;
            
                    processQueue.Add(nextProcess);
                    remainingProcesses.Remove(nextProcess);
                }
            
                totalTime.Stop();
                LogProcessesDetails(processQueue);
                LogProcessPerformance(processQueue.Count, totalTime);
            } 
            else {
                Console.WriteLine("No processes provided");
            }
        }

        
        //Calculation Utilities
        public static double CalculateAvgTurnAroundTime(List<Process> processes) {
            return Math.Round(processes.Average(process => process.TurnaroundTime), 2);
        }
        public static double CalculateAvgWaitTime(List<Process> processes) {
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
        static void LogProcessesDetails(List<Process> processes) {
            Console.WriteLine("\n======== Scheduling Summary ====================");
            Console.WriteLine("P \tAT \tBT \tCT \tTAT \tWT");
            foreach(Process p in processes){
                Console.WriteLine($"{p.Id} \t{p.ArrivalTime} \t{p.BurstTime} \t{p.CompletionTime} \t{p.TurnaroundTime} \t{p.WaitTime}");
            }

            Console.WriteLine("__________________________________________________\n");
            Console.WriteLine($"Total Processes: {processes.Count}");
            Console.Write("Order: ");
            foreach(Process p in processes){
                Console.Write($"{p.Id} ");
            }
            Console.WriteLine($"\nAvg. Turnaround Time: {CalculateAvgTurnAroundTime(processes)}");
            Console.WriteLine($"Avg. Wait Time: {CalculateAvgWaitTime(processes)}");
        }
        static void LogProcessPerformance(int totalProcesses, Stopwatch totalTime) {

            /*Performance Metrics:
                - Average Waiting Time (AWT)
                - Average Turnaround Time (ATAT)
                - CPU Utilization (%) = (Total Busy Time / Total Time) * 100
                    Total Busy Time is the time the CPU spends executing processes. 
                    Total Time is the total observation period, including idle time.
                - Throughput = Total Number of Processes Completed / Total Time
                    Total Number of Processes Completed refers to the number of processes that have finished execution.
                    Total Time is the observation period during which the processes were executed.
            */
            Console.WriteLine("__________________________________________________\n");
            Console.WriteLine("Performance Metrics");
            Console.WriteLine($"Total Time: {totalTime.ElapsedMilliseconds} ms ");
            Console.WriteLine($"TODO ===  Busy Time: ms");
            Console.WriteLine($"TODO === CPU Utilization: %");
            Console.WriteLine($"TODO === Throughput:");
            Console.WriteLine("__________________________________________________\n");
        }
    }
}