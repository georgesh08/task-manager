namespace TaskManager.Entities;

public abstract class AbstractTask
{
    public Guid Id { get; set; }
    public string Information { get; private set; } 
    public bool IsCompleted { get; private set; }

    public void CompleteTask()
    {
        IsCompleted = true;
    }

    public void UpdateInformation(string newInfo)
    {
        Information = newInfo;
    }
}