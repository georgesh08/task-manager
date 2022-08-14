namespace TaskManager.Entities;

public class ExportSubtask
{
    public int Id { get; set; }
    public string Information { get; set; }
    public bool IsCompleted { get; set; }
    
    public ExportSubtask() { }

    public ExportSubtask(int id, string information, bool isCompleted)
    {
        Id = id;
        Information = information;
        IsCompleted = isCompleted;
    }
}