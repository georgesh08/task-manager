namespace TaskManager.Entities;

public abstract class AbstractTask
{
    public Guid Id { get; private set; }
    public string Information { get; private set; } = "";
    public bool IsCompleted { get; private set; }

    public AbstractTask()
    {
        Id = Guid.NewGuid();
    }

    public void CompleteTask()
    {
        IsCompleted = true;
    }

    public void UpdateInformation(string newInfo)
    {
        Information = newInfo;
    }
}