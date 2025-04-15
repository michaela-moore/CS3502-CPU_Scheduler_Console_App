using System.Collections;
using System.Diagnostics;
using System.IO.Pipelines;

public class Algorithms
{
    /*
        Already impleented... 
        - First Come First Serve (FCFS)
        - Shortest Job First (SJF)
        - Round Robin (RR)
        - Priority Scheduling (PS)
    */



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
        PrintProcessDetails(processes);
        PrintProcessPerformance(numberOfProcesses, totalTime);
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

        Stopwatch totalTime = new();
        totalTime.Start();

        //TESTING
        PrintProcesses("Given list ", processes);
        

        List<Process> processesQueued = [];
        List<Process> remainingProcesses = new List<Process>(processes);
        remainingProcesses.Sort((a, b) => a.ArrivalTime.CompareTo(b.ArrivalTime)); //Only considers AT

        Process currentProcess;
        Process lastQueued;

        //Run -> 

        //Get the first process and add to queue 
        for( int i = 0; i < remainingProcesses.Count; i++){

            //Establish first process
            if(i == 0) {
                currentProcess = remainingProcesses[0];
                currentProcess.CompletionTime = currentProcess.ArrivalTime + currentProcess.BurstTime;
                processesQueued.Add(currentProcess);
                remainingProcesses.Remove(remainingProcesses[0]);  
            }
            
            //Get last process in the queue if not empty
            if(processesQueued.Count != 0) {
                lastQueued = processesQueued[processesQueued.Count - 1];

            //Calculate CT & RR from last queued process
                while(remainingProcesses.Count != 0){

                    //TESTING 
                    PrintProcesses("Remaining proce ", remainingProcesses);

                    foreach(Process process in remainingProcesses){
                        process.CompletionTime = process.ArrivalTime + lastQueued.CompletionTime;
                    }

                    //sort remaining process by their response 
                    remainingProcesses.Sort((a,b) => b.ResponseRatio.CompareTo(a.ResponseRatio));
                    Console.WriteLine($"Highest Response Rate = ");
                    
                    processesQueued.Add(remainingProcesses[0]);
                    remainingProcesses.Remove(remainingProcesses[0]);


                }

                

             } 
             

        }


        //if processes all arrive at the same time, sort by burst time (smallest to largest) and select the first process in the list
        // then can calculate response ratio for the remaining processes in the list

        //if AT is not the same, sort by AT (smallest to largest) and select the first process in the list

        


        totalTime.Stop();
        PrintProcessDetails(processesQueued);
        //PrintProcessPerformance(processesQueued.Count, totalTime);
    }

        
     public static void PrintProcessDetails(List<Process> processes) {
        Console.WriteLine("P \tAT \tBT \tCT \tTAT \tWT ");
        foreach(Process process in processes){
            Console.WriteLine($"{process.ProcessId} \t{process.ArrivalTime} \t{process.BurstTime} \t{process.CompletionTime} \t{process.TurnaroundTime} \t{process.WaitTime}");
        }

        Console.WriteLine("=====================================");
        Console.WriteLine($"Total Processes: {processes.Count}");
        Console.WriteLine($"Avg. Turnaround Time: {processes.Average(p => p.TurnaroundTime)}");
        Console.WriteLine($"Avg. Wait Time: {processes.Average(p => p.WaitTime)}");
    }

    public static void PrintProcessPerformance(int totalProcesses, Stopwatch totalTime) {

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

        Console.WriteLine("\nPerformance Metrics");
        Console.WriteLine($"Total Time: {totalTime.ElapsedMilliseconds} ms ");
        Console.WriteLine($"TODO ===  Busy Time: ms");
        Console.WriteLine($"TODO === CPU Utilization: %");
        Console.WriteLine($"TODO === Throughput: \n");
    }

    //TESTING 
    public static void PrintProcesses(String details, List<Process> processes){

        Console.WriteLine("\t\t\tP \t AT \t BT \t CT \t RR");
        foreach(Process p in processes){
            Console.WriteLine($" {details} \t{p.ProcessId} \t {p.ArrivalTime} \t {p.BurstTime} \t {p.CompletionTime} \t {p.ResponseRatio}");
        }
    }


}