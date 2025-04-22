namespace CPU_SCHEDULING {

    using System;
    using System.Diagnostics;
    using System.Threading;
    using CPU_SCHEDULING.Models;

    public class Performance {

        public static void LogCPUU(Action function) {
       
            double cpuUsage = 0;
            int processCountBefore = System.Diagnostics.Process.GetProcesses().Length;
            int processCountAfter = processCountBefore; 

            if(OperatingSystem.IsWindows()) {

                try {
                    PerformanceCounter cpuCounter = new("Processor", "% Processor Time", "_Total");

                    cpuCounter.NextValue();

                    Thread.Sleep(1000);
                    
                    //Initiate function
                    function();
                    
                    //Get values
                    cpuUsage = cpuCounter.NextValue();
                    processCountAfter = System.Diagnostics.Process.GetProcesses().Length;
           
                }
                catch(InvalidOperationException e) {
                    Console.WriteLine(e);
                }

            }
             else {
                //Initiate function
                function();
             }

            Console.WriteLine($"CPU Utilization: {Math.Round(cpuUsage, 2)} %");
            Console.WriteLine($"Processes/second: {processCountAfter - processCountBefore} ");

        }
    }
}