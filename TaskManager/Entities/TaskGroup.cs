namespace TaskManager.Entities;

public class TaskGroup
{
    public Guid Id { get; private set; }
    public string Name { get; set; }

    public TaskGroup(string name)
    {
        Name = name;
    }
    public TaskGroup()
    {
        Id = Guid.NewGuid();
    }
}