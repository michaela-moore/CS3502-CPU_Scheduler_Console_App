namespace CPU_Scheduling_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CPU_SCHEDULING.Models;

[TestClass]
public sealed class ShortestJobFirstTest
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
        Algorithms.ShortestJobFirst(testProcesses);
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

        double EXPECTED_AVG_TAT = 13;
        double EXPECTED_AVG_WT = 7;
        //string[] EXPECTED_PROCESS_ORDER = ["P4", "P1", "P3", "P2"];
        
        //Run Scheduling Calcs
        Algorithms.ShortestJobFirst(testProcesses);
        double calculated_AVG_TAT = Algorithms.CalculateAvgTurnAroundTime(testProcesses);
        double calculated_AVG_WT = Algorithms.CalculateAvgWaitTime(testProcesses);
        

        //Outcome
        Assert.AreEqual(EXPECTED_AVG_WT, calculated_AVG_WT);
        Assert.AreEqual(EXPECTED_AVG_TAT, calculated_AVG_TAT);
    }

     [TestMethod]
    public void EC1_Processes_WithSameBT_SameAT()
    {
        Console.WriteLine("Edge Case (1) All processes arrive at time 0 with identical burst times");

        List<Process> EC_1 =
            [
                new Process("P1", 0, 6), 
                new Process("P2", 0, 6),
                new Process("P3", 0, 6), 
                new Process("P4", 0, 6),
                new Process("P5", 0, 6), 
            ];

        double EXPECTED_AVG_TAT = 18;
        double EXPECTED_AVG_WT = 12;
        
        //Run Scheduling Calcs
        Algorithms.ShortestJobFirst(EC_1);
        double calculated_AVG_TAT = Algorithms.CalculateAvgTurnAroundTime(EC_1);
        double calculated_AVG_WT = Algorithms.CalculateAvgWaitTime(EC_1);
        

        //Outcome
        Assert.AreEqual(EXPECTED_AVG_WT, calculated_AVG_WT);
        Assert.AreEqual(EXPECTED_AVG_TAT, calculated_AVG_TAT);
    }

    [TestMethod]
    public void EC2_Processes_WithMixedSized_AT_and_BT()
    {
        Console.WriteLine("Edge Case (2) Extremely long burst times mixed with very short burst times.");
        List<Process> EC_2 =
            [
                new Process("P1", 1, 1), 
                new Process("P2", 15, 50),
                new Process("P3", 3, 4), 
                new Process("P4", 43, 100),
                new Process("P5", 5, 1), 
                new Process("P6", 12, 32), 
            ];

        double EXPECTED_AVG_TAT = 45;
        double EXPECTED_AVG_WT = 13.67;
        
        //Run Scheduling Calcs
        Algorithms.ShortestJobFirst(EC_2);
        double calculated_AVG_TAT = Algorithms.CalculateAvgTurnAroundTime(EC_2);
        double calculated_AVG_WT = Algorithms.CalculateAvgWaitTime(EC_2);
        

        //Outcome
        Assert.AreEqual(EXPECTED_AVG_WT, calculated_AVG_WT);
        Assert.AreEqual(EXPECTED_AVG_TAT, calculated_AVG_TAT);
    }
}
