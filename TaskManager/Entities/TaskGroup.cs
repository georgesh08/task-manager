namespace TaskManager.Entities;

public class TaskGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Task> Tasks { get; set; }
}