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

                    Console.WriteLine("Stopping...");

                } else {
                        Console.WriteLine("\n...working...\n");

                        List<Process> processesWithAt = GenerateProcessesWithAT(totalProcesses);
                        //List<Process> processesNoAt = GenerateProcessesWithoutAT(totalProcesses);

                        Performance.LogCPUU(()=> Algorithms.ShortestRemainingTimeFirst(processesWithAt));
                        Performance.LogCPUU(()=> Algorithms.HighestResponseRatio(processesWithAt));
                        Performance.LogCPUU(()=> Algorithms.FirstComeFirstServed(processesWithAt));
                        Performance.LogCPUU(()=> Algorithms.ShortestJobFirst(processesWithAt));
                }
            } 

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