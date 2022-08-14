using System.ComponentModel.DataAnnotations;
using TaskManager.Generators;

namespace TaskManager.Entities;

public abstract class AbstractTask
{
    [Key]
    public Guid Id { get; private set; }
    public int UserId { get; private set; }
    public string Information { get; private set; } = "";
    public bool IsCompleted { get; private set; }
    
    public AbstractTask()
    {
        Id = Guid.NewGuid();
        UserId = IdGenerator.GetInstance().GenerateTaskId();
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