namespace CPU_SCHEDULING.Models
{

    public class Scheduler
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("\nCPU Scheduling Algorithms Comparison");
            Console.WriteLine("=====================================");
            bool programActive = true;

            while(programActive){
                Console.WriteLine($"\nEnter number of processes to schedule: [0 or any KEY to exit] ");
                    string totalNumberOfProcesses = Console.ReadLine() ?? "";

                    bool isValidNumber = int.TryParse(totalNumberOfProcesses, out int totalProcesses);

                if(totalProcesses == 0 || !isValidNumber) {
                    programActive = false;

                    Console.WriteLine("Stoping...");

                } else {
                        Console.WriteLine("\n...working...\n");

                        List<Process> processesWithAt = GenerateProcessesWithAT(totalProcesses);
                        //List<Process> processesNoAt = GenerateProcessesWithoutAT(totalProcesses);

                        Performance.LogCPUU(()=> Algorithms.FirstComeFirstServed(processesWithAt));
                        Performance.LogCPUU(()=> Algorithms.ShortestJobFirst(processesWithAt));
                        Performance.LogCPUU(()=> Algorithms.HighestResponseRatio(processesWithAt));
                        Performance.LogCPUU(()=> Algorithms.ShortestRemainingTimeFirst(processesWithAt));

                        //Algorithms.ShortestRemainingTimeFirst(processesWithAt);
                        //Algorithms.FirstComeFirstServed(processesWithAt);
                        //Algorithms.ShortestJobFirst(processesWithAt);
                        //Algorithms.HighestResponseRatio(processesWithAt);

                        //Performance.LogCPUU(()=> Algorithms.ShortestRemainingTimeFirst(processesNoAt));
                        //Performance.LogCPUU(()=> Algorithms.FirstComeFirstServed(processesNoAt));
                        //Performance.LogCPUU(()=> Algorithms.ShortestJobFirst(processesNoAt));
                        //Performance.LogCPUU(()=> Algorithms.HighestResponseRatio(processesNoAt));
                }
                
            } 
            List<Process> EC_1 =
            [
                new Process("P1", 0, 6), 
                new Process("P2", 0, 6),
                new Process("P3", 0, 6), 
                new Process("P4", 0, 6),
                new Process("P5", 0, 6), 
            ];
            Console.WriteLine("\n=====================================");
            Console.WriteLine("Edge Case (1) All processes arrive at time 0 with identical burst times");
            Console.WriteLine("======================================="); 
            Performance.LogCPUU(()=> Algorithms.FirstComeFirstServed(EC_1));
            Performance.LogCPUU(()=> Algorithms.ShortestJobFirst(EC_1));
            Performance.LogCPUU(()=> Algorithms.HighestResponseRatio(EC_1));
            Performance.LogCPUU(()=> Algorithms.ShortestRemainingTimeFirst(EC_1));

            List<Process> EC_2 =
            [
                new Process("P1", 1, 1), 
                new Process("P2", 15, 50),
                new Process("P3", 3, 4), 
                new Process("P4", 123, 1100),
                new Process("P5", 5, 1), 
                new Process("P6", 12, 32),
            ];  
            Console.WriteLine("\n=======================================");
            Console.WriteLine("Edge Case (2) Extremely long burst times mixed with very short burst times.");
            Console.WriteLine("=========================================");
            Performance.LogCPUU(()=> Algorithms.FirstComeFirstServed(EC_2));
            Performance.LogCPUU(()=> Algorithms.ShortestJobFirst(EC_2));
            Performance.LogCPUU(()=> Algorithms.HighestResponseRatio(EC_2));
            Performance.LogCPUU(()=> Algorithms.ShortestRemainingTimeFirst(EC_2));
        }
        


        
        /* ----------------------------------------- HELPERS -----------------------------------------*/
        public static List<Process> GenerateProcessesWithoutAT(int totalProcesses, int minBurstTime, int maxBurstTime) {

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
        public static List<Process> GenerateProcessesWithoutAT(int totalProcesses) {

            List<Process> processes = [];
            Random random = new();

            for (int i = 0; i < totalProcesses; i++)
            {
                string processId = "P" + (i + 1).ToString();
                int burstTime = random.Next(1, totalProcesses*5);
                processes.Add(new Process(processId, burstTime));
            }

            return processes;
        }
        //Generates processes with random burst times and arrival times
        public static List<Process> GenerateProcessesWithAT(int totalProcesses) {
            List<Process> processes = [];
            Random random = new();

            for (int i = 0; i < totalProcesses; i++)
            {
                string processId = "P" + (i + 1).ToString();
                int burstTime = random.Next(1, (totalProcesses * 2)); 
                int arrivalTime = random.Next(0, (totalProcesses * 2));
                processes.Add(new Process(processId, burstTime, arrivalTime));
            }

            return processes;
        }

        //Generates processes with min/max burst times and random arrival times
        public static List<Process> GenerateProcessesWithAT(int totalProcesses, int minBurstTime, int maxBurstTime) {
            List<Process> processes = [];
            Random random = new();

            for (int i = 0; i < totalProcesses; i++) {
                string processId = "P" + (i + 1).ToString();
                int burstTime = random.Next(minBurstTime, maxBurstTime); // Random burst time between minBurstTime and max
                int arrivalTime = random.Next(0, (totalProcesses * 2));
                processes.Add(new Process(processId, burstTime, arrivalTime));
            }
            return processes;
        }

    }
}