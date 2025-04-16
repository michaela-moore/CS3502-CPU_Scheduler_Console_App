public class Process 
{
    readonly string _id;
    readonly int arrivalTime;
    readonly int burstTime;
    int remainingTime; //if preemptive (ie: SRTF)
    int completionTime;

    
    public Process(string processId, int arrivalTime, int burstTime) {
        _id = processId;
        this.arrivalTime = arrivalTime;
        this.burstTime = burstTime;
    }


    public Process(string processId,  int burstTime) {
        _id = processId;
        this.burstTime = burstTime;
        this.arrivalTime = 0; // default arrival time
    }

     public string Id
    {
        get { return _id; }
    }
    
    public int ArrivalTime
    {
        get { return arrivalTime; }
    }

    public int BurstTime
    {
        get { return burstTime; }
    }

    public int RemainingTime
    {
        get { return remainingTime; }
        set { remainingTime = value; }
    }

    public int CompletionTime
    {
        get { return completionTime; }
        set { completionTime = value; }
    }

    public int TurnaroundTime
    {
        get {return completionTime - arrivalTime; }
    }

    public int WaitTime
    {
        get { return TurnaroundTime - burstTime; }
    }
    
    public double ResponseRatio {
        get { return ((WaitTime + (double) burstTime) / burstTime) ; }  
    }
}