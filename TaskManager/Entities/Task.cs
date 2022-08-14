namespace TaskManager.Entities;

public class Task : AbstractTask
{
    public DateOnly Deadline { get; private set; }
    public TaskGroup? Group { get; set; }
    
    public void SetDeadline(DateOnly deadline)
    {
        Deadline = deadline;
    }
}