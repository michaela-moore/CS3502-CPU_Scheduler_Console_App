namespace CPU_Scheduling_Tests;
using CPU_SCHEDULING.Models;

[TestClass]
public sealed class ShortestRemainingTimeFirstTest
{

    [TestMethod]
    public void Processes_WithVariousBT_VariousAT()
    {
        List<Process> testProcesses =
        [
            new Process("P1", 0, 8), 
            new Process("P2", 1, 4),
            new Process("P3", 2, 9),
            new Process("P4", 3, 5),
        ];

        double EXPECTED_AVG_TAT = 13;
        double EXPECTED_AVG_WT = 6.5;
        
        //Run Scheduling Calcs
        Algorithms.ShortestRemainingTimeFirst(testProcesses);
        double calculated_AVG_TAT = Algorithms.CalculateAvgTurnAroundTime(testProcesses);
        double calculated_AVG_WT = Algorithms.CalculateAvgWaitTime(testProcesses);


        //Outcome
        Assert.AreEqual(EXPECTED_AVG_WT, calculated_AVG_WT);
        Assert.AreEqual(EXPECTED_AVG_TAT, calculated_AVG_TAT);
    }

    [TestMethod]
    public void Processes_WithVariousBT_AllATSame() //Accounts for no AT given (defaults to 0)
    {
        //STRF will act as SJF if all the ATs are the same
        
        List<Process> testProcesses =
        [
            new Process("P1", 6), 
            new Process("P2", 8),
            new Process("P3", 7),
            new Process("P4", 3),
        ];

        double EXPECTED_AVG_TAT = 13;
        double EXPECTED_AVG_WT = 7;
        
        //Run Scheduling Calcs
        Algorithms.ShortestRemainingTimeFirst(testProcesses);
        double calculated_AVG_TAT = Algorithms.CalculateAvgTurnAroundTime(testProcesses);
        double calculated_AVG_WT = Algorithms.CalculateAvgWaitTime(testProcesses);
        

        //Outcome
        Assert.AreEqual(EXPECTED_AVG_WT, calculated_AVG_WT);
        Assert.AreEqual(EXPECTED_AVG_TAT, calculated_AVG_TAT);
    }
}
