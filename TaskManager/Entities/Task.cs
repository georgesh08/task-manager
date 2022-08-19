namespace TaskManager.Entities;

public class Task : AbstractTask
{
    public DateTime Deadline { get; private set; } = DateTime.MinValue;
    public TaskGroup? Group { get; set; }
    
    public void SetDeadline(DateTime deadline)
    {
        Deadline = deadline.Date;
    }
}