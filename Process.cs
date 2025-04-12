public class Process {

    string processId;
    int arrivalTime;
    int burstTime;
    
    public Process(string processId, int arrivalTime, int burstTime) {
        this.processId = processId;
        this.arrivalTime = arrivalTime;
        this.burstTime = burstTime;
    }

    public Process(string processId,  int burstTime) {
        this.processId = processId;
        this.burstTime = burstTime;
    }
    
}