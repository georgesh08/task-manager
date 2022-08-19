namespace TaskManager.Entities;

public class ExportGroup
{
    public string Name { get; set; }
    public int Id { get; set; }
    public List<ExportTask> Tasks { get; set; } = new();
    
    public ExportGroup() { }

    public ExportGroup(TaskGroup group)
    {
        Name = group.Name;
        Id = group.UserId;
    }
}