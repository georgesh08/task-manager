using System.ComponentModel.DataAnnotations;
using TaskManager.Generators;

namespace TaskManager.Entities;

public class TaskGroup
{
    [Key]
    public Guid Id { get; private set; }
    public int UserId { get; private set; }
    public string Name { get; }
    
    public TaskGroup() { }

    public TaskGroup(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
        UserId = IdGenerator.GetInstance().GenerateGroupId();
    }
}