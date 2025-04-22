namespace CPU_Scheduling_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CPU_SCHEDULING.Models;

[TestClass]
public sealed class HighestResponseRateAlgoTest
{
    [TestMethod]
    public void Processes_WithVariousBT_And_BT()
    {
        List<Process> testProcesses =
        [
            new Process("P1", 0, 6), 
            new Process("P2", 4, 10),
            new Process("P3", 4, 4),
            new Process("P4", 8, 5),
        ];

        double EXPECTED_AVG_TAT = 11.25;
        double EXPECTED_AVG_WT = 5;
        //string[] EXPECTED_PROCESS_ORDER = ["P1", "P3", "P2", "P4"];

        //Run Scheduling Calcs
        Algorithms.HighestResponseRatio(testProcesses);
        double calculated_AVG_TAT = Algorithms.CalculateAvgTurnAroundTime(testProcesses);
        double calculated_AVG_WT = Algorithms.CalculateAvgWaitTime(testProcesses);
        
        //Outcome
        Assert.AreEqual(EXPECTED_AVG_TAT, calculated_AVG_TAT);
        Assert.AreEqual(EXPECTED_AVG_WT, calculated_AVG_WT);

    }

    [TestMethod]
    public void Processes_WithVariousBT_AllDefaultAT(){
        List<Process> testProcesses =
        [
            new Process("P1", 0, 4), 
            new Process("P2", 0, 3),
            new Process("P3", 0, 2),
        ];

        double EXPECTED_AVG_TAT = 5.33;
        double EXPECTED_AVG_WT = 2.33;
        //string[] EXPECTED_PROCESS_ORDER = ["P1", "P2", "P3"];

        //Run Scheduling Calcs
        Algorithms.HighestResponseRatio(testProcesses);
        double calculated_AVG_TAT = Algorithms.CalculateAvgTurnAroundTime(testProcesses);
        double calculated_AVG_WT = Algorithms.CalculateAvgWaitTime(testProcesses);
        
        //Outcome
        Assert.AreEqual(EXPECTED_AVG_TAT, calculated_AVG_TAT);
        Assert.AreEqual(EXPECTED_AVG_WT, calculated_AVG_WT);
    }

    [TestMethod]
    public void Processes_WithVariousBT_SomeWithSameAT(){
        List<Process> testProcesses =
        [
            new Process("P1", 0, 2), 
            new Process("P2", 0, 3),
            new Process("P3", 3, 1),
        ];

        double EXPECTED_AVG_TAT = 3.33;
        double EXPECTED_AVG_WT = 1.33;
        //string[] EXPECTED_PROCESS_ORDER = ["P1", "P2", "P3"];

        //Run Scheduling Calcs
        Algorithms.HighestResponseRatio(testProcesses);
        double calculated_AVG_TAT = Algorithms.CalculateAvgTurnAroundTime(testProcesses);
        double calculated_AVG_WT = Algorithms.CalculateAvgWaitTime(testProcesses);
        
        //Outcome
        Assert.AreEqual(EXPECTED_AVG_TAT, calculated_AVG_TAT);
        Assert.AreEqual(EXPECTED_AVG_WT, calculated_AVG_WT);
    }

}
