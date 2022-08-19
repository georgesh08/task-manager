namespace TaskManager.Entities;

public class ExportTask
{
    public int GroupId { get; set; } = -1;
    public int Id { get; set; }
    public string Information { get; set; }
    public bool IsCompleted { get; set; }
    public string Deadline { get; set; } = "-";
    public List<ExportSubtask> RelatedSubtasks { get; set; } = new();
    
    public ExportTask() { }

    public ExportTask(Task task)
    {
        GroupId = task.Group?.UserId ?? -1;
        Id = task.UserId;
        Information = task.Information;
        IsCompleted = task.IsCompleted;
        if (task.Deadline != DateTime.MinValue)
        {
            Deadline = task.Deadline.ToString();
        }
    }
}