namespace TaskManager.Entities;

public class ExportTask
{
    public int GroupId { get; set; }
    public int Id { get; set; }
    public string Information { get; set; }
    public bool IsCompleted { get; set; }
    public string Deadline { get; set; }
    public List<ExportSubtask> RelatedSubtasks { get; set; } = new();
    
    public ExportTask() { }

    public ExportTask(TaskGroup? group, int id, string info, bool isCompleted, DateOnly deadline)
    {
        GroupId = group?.UserId ?? -1;
        Id = id;
        Information = info;
        IsCompleted = isCompleted;
        Deadline = deadline == DateOnly.MinValue ? "-" : deadline.ToString();
    }
}