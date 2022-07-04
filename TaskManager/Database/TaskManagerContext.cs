using System.Data.Entity;
using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Database;

public class TaskManagerContext : DbContext
{
    public TaskManagerContext() { }
    public TaskManagerContext(string connectionString) : base (connectionString) { }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Subtask> Subtasks { get; set; }
    public DbSet<TaskGroup> TaskGroups { get; set; }
}