using System.ComponentModel.DataAnnotations;
using TaskManager.Generators;

namespace TaskManager.Entities;

public class TaskGroup
{
    public Guid Id { get; private set; }
    [Key]
    public int UserId { get; private set; }
    public string Name { get; }

    public TaskGroup(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
        UserId = GroupIdGenerator.GetInstance().GenerateId();
    }
}