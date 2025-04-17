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
        */
        public static void HighestResponseRatio(List<Process> processes) {

            //Ensures list is not empty/null 
            if(processes.Count > 0){

                Stopwatch totalTime = new();
                totalTime.Start();

                List<Process> processesQueued = new List<Process>();
                List<Process> remainingProcesses = new List<Process>(processes);

                //TODO 
                //Orders by AT. Does not consider other variables such as BT if more than process has the same AT
                remainingProcesses.Sort((a, b) => a.ArrivalTime.CompareTo(b.ArrivalTime)); 

                
                //Initiate processes ---- 
                Process currentProcess;

                for( int i = 0; i < remainingProcesses.Count; i++){
                    
                    currentProcess = remainingProcesses[i];

                    //First process runs based on its AT which determines its CT
                    currentProcess.CompletionTime = currentProcess.ArrivalTime + currentProcess.BurstTime;
                    processesQueued.Add(currentProcess);
                    remainingProcesses.Remove(currentProcess);

                    //Evaluate remaining processes
                    while(remainingProcesses.Count > 0) {
                        Process lastQueued = processesQueued[processesQueued.Count - 1];

                        //Calculate the RR for remaining processes to determine sequencing
                        Console.WriteLine("\nCalculating response ratios for remaining processes...");    
                        
                        foreach(Process process in remainingProcesses){
                            //CT is used to calc TAT, which is used to calc WT, which is needed to calc RR
                            //(Highest) Response Rate (RR) is used to determine the next process to sequence
                            process.CompletionTime = process.BurstTime + lastQueued.CompletionTime;
                            
                            LogProcessInfo(process);
                        }

                        //Find the process with the largest RR and add to queue
                        Process largestResponseRate = remainingProcesses.OrderByDescending(p => p.ResponseRatio).First();
                        processesQueued.Add(largestResponseRate);
                        remainingProcesses.Remove(largestResponseRate);
                    }
                }
                    totalTime.Stop();
                    LogProcessesDetails(processesQueued);
                    LogProcessPerformance(processesQueued.Count, totalTime);
            } 
            else {
                Console.WriteLine("No processes provided");
            }
        }

        
        //Calculation Utilities

        public static double CalculateAvgTurnAroundTime(List<Process> processes) {
            return processes.Average(process => process.TurnaroundTime);
        }
        public static double CalculateAvgWaitTime(List<Process> processes) {
            return processes.Average(process => process.WaitTime);
        }
        
        //Logging Utilities
        static void LogProcessInfo(Process p) {
            Console.WriteLine($"{p.Id} \tAT {p.ArrivalTime} \tBT {p.BurstTime} \tCT {p.CompletionTime} \tTAT {p.TurnaroundTime} \tWT {p.WaitTime} \tRR {p.ResponseRatio}");
        }    
        static void LogProcessesDetails(List<Process> processes) {
            Console.WriteLine("'\n======== Scheduling Summary ====================");
            Console.WriteLine("P \tAT \tBT \tCT \tTAT \tWT \tRR");
            foreach(Process p in processes){
                Console.WriteLine($"{p.Id} \t{p.ArrivalTime} \t{p.BurstTime} \t{p.CompletionTime} \t{p.TurnaroundTime} \t{p.WaitTime} \t{p.ResponseRatio}");
            }

            Console.WriteLine("__________________________________________________\n");
            Console.WriteLine($"Total Processes: {processes.Count}");
            Console.WriteLine($"Avg. Turnaround Time: {CalculateAvgTurnAroundTime(processes)}");
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