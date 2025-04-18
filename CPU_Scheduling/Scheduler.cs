namespace CPU_SCHEDULING.Models
{
    public class Scheduler
    {
        public static void Main(string[] args)
        {
          Console.WriteLine("\nCPU Scheduling Algorithms Comparison");
          Console.WriteLine("=====================================");


            //Test Case 
            List<Process> processes =
            [
                new Process("P1", 6), //Process id and burst time
                new Process("P2", 8),
                new Process("P3", 7),
                new Process("P4", 3),
            ];
          
            List<Process> processesWithAT =
            [
                new Process("P1", 0, 6), //Process id and burst time
                new Process("P2", 4, 10),
                new Process("P3", 4, 4),
                new Process("P4", 8, 5),
            ];

            List<Process> testProcesses2 =
        [
            new Process("P1", 0, 2), 
            new Process("P2", 0, 3),
            new Process("P3", 3, 1),
        ];

            //Number of processes, minburst time, maxburst time 
            List<Process> randomProcessList  = GenerateProcesses(4, 1, 10);


            //Console.WriteLine("Shortest Job First"); //non-preemptive 
            //Algorithms.ShortestJobFirst(processes);


            /* COMPARISONS */
            //Console.WriteLine("SHORTEST JOB FIRST --------");
            //Algorithms.ShortestJobFirst(randomProcessList);
            
            Console.WriteLine("\nHIGHEST RESPONSE RATIO --------------");
            Algorithms.HighestResponseRatio(testProcesses2);


            //Console.WriteLine("\nShortest Remaining Time First --------------");
            //Algorithms.ShortestRemainingTimeFirst(processesWithAT);






            //Test Cases
            // 10 Processes
            // 100 Processes
            // 1000 Processes
            // 10000 Processes
            // 20 Processes with  min 0 - max 1000 burst times
            // AT = 0 & BT are same

        }

        public static List<Process> GenerateProcesses(int totalProcesses, int minBurstTime, int maxBurstTime) {

            List<Process> processes = [];
            Random random = new();

            for (int i = 0; i < totalProcesses; i++)
            {
                string processId = "P" + (i + 1).ToString();
                int burstTime = random.Next(minBurstTime, maxBurstTime); // Random burst time between minBurstTime and max
                processes.Add(new Process(processId, burstTime));
            }

            return processes;

        }

   

    }
}