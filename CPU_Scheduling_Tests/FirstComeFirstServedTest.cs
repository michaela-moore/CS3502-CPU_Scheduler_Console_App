namespace CPU_Scheduling_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CPU_SCHEDULING.Models;

[TestClass]
public sealed class FirstComeFirstServedTest
{

    [TestMethod]
    public void Processes_WithVariousBT_VariousAT()
    {
        List<Process> testProcesses =
        [
            new Process("P1", 2, 4), 
            new Process("P2", 0, 2),
            new Process("P3", 3, 1),
            new Process("P4", 5, 3),
        ];

        double EXPECTED_AVG_TAT = 3.75;
        double EXPECTED_AVG_WT = 1.25;
        //string[] EXPECTED_PROCESS_ORDER = ["P2", "P1", "P3", "P4"];
        
        //Run Scheduling Calcs
        Algorithms.FirstComeFirstServed(testProcesses);
        double calculated_AVG_TAT = Algorithms.CalculateAvgTurnAroundTime(testProcesses);
        double calculated_AVG_WT = Algorithms.CalculateAvgWaitTime(testProcesses);
        
        //Outcome
        Assert.AreEqual(EXPECTED_AVG_WT, calculated_AVG_WT);
        Assert.AreEqual(EXPECTED_AVG_TAT, calculated_AVG_TAT);

    }

    [TestMethod]
    public void Processes_WithVariousBT_AllATSame() //Accounts for no AT given (defaults to 0)
    {
        List<Process> testProcesses =
        [
            new Process("P1", 6), 
            new Process("P2", 8),
            new Process("P3", 7),
            new Process("P4", 3),
        ];

        double EXPECTED_AVG_TAT = 16.25;
        double EXPECTED_AVG_WT = 10.25;
        //string[] EXPECTED_PROCESS_ORDER = ["P1", "P2", "P3", "P4"];
        
        //Run Scheduling Calcs
        Algorithms.FirstComeFirstServed(testProcesses);
        double calculated_AVG_TAT = Algorithms.CalculateAvgTurnAroundTime(testProcesses);
        double calculated_AVG_WT = Algorithms.CalculateAvgWaitTime(testProcesses);
        
        //SJF targets jobs with the shortest BT, so order by BT
        string[] orderOfProcesses = testProcesses.OrderBy(p => p.BurstTime).Select(p => p.Id).ToArray();

        //Outcome
        Assert.AreEqual(EXPECTED_AVG_WT, calculated_AVG_WT);
        Assert.AreEqual(EXPECTED_AVG_TAT, calculated_AVG_TAT);
    }

    [TestMethod]
    public void Processes_WithVariousBT_SequentialAT()
    {
        List<Process> testProcesses =
        [
            new Process("P1", 1, 4), 
            new Process("P2", 3, 6),
            new Process("P3", 2, 3),
            new Process("P4", 6, 1),
        ];

        double EXPECTED_AVG_TAT = 7.5;
        double EXPECTED_AVG_WT = 4;
        //string[] EXPECTED_PROCESS_ORDER = ["P1", "P3", "P2", "P4"];
        
        //Run Scheduling Calcs
        Algorithms.FirstComeFirstServed(testProcesses);
        double calculated_AVG_TAT = Algorithms.CalculateAvgTurnAroundTime(testProcesses);
        double calculated_AVG_WT = Algorithms.CalculateAvgWaitTime(testProcesses);
        

        //Outcome
        Assert.AreEqual(EXPECTED_AVG_WT, calculated_AVG_WT);
        Assert.AreEqual(EXPECTED_AVG_TAT, calculated_AVG_TAT);
    }
}
