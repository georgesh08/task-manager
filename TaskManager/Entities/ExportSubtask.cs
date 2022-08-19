namespace TaskManager.Entities;

public class ExportSubtask
{
    public int Id { get; set; }
    public string Information { get; set; }
    public bool IsCompleted { get; set; }
    
    public ExportSubtask() { }

    public ExportSubtask(Subtask subtask)
    {
        Id = subtask.UserId;
        Information = subtask.Information;
        IsCompleted = subtask.IsCompleted;
    }
}