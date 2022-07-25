using System.Data.Entity;
using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Database;

public class TaskManagerContext : DbContext, IDatabase
{
    public DbSet<Task?> Tasks { get; set; }
    public DbSet<Subtask?> Subtasks { get; set; }
    public DbSet<TaskGroup?> TaskGroups { get; set; }
    public TaskManagerContext() { }
    public TaskManagerContext(string connectionString) : base (connectionString) { }

    public void AddTask(Task task)
    {
        Tasks.Add(task); 
        SaveChanges();
    }

    public void AddSubtask(Subtask subtask)
    {
        Subtasks.Add(subtask);
        SaveChanges();
    }

    public void AddTaskGroup(TaskGroup group)
    {
        TaskGroups.Add(group);
        SaveChanges();
    }

    public void AddTaskToGroup(Guid taskId, Guid groupId)
    {
        Task<TaskGroup?> group = TaskGroups.SingleOrDefaultAsync(g => g != null && g.Id.Equals(groupId));
        Task<Task?> task = Tasks.SingleOrDefaultAsync(t => t != null && t.Id.Equals(taskId));
        if (task.Result != null) task.Result.Group = group.Result;
        SaveChanges();
    }

    public void DeleteTask(Guid taskId)
    {
        Task? task = Tasks.Find(taskId);
        var subTasks = Subtasks.ToList();
        foreach (Subtask? subtask in subTasks.Where(subtask => subtask?.Task != null && subtask.Task.Equals(task)))
        {
            if (subtask != null) Subtasks.Remove(subtask);
        }

        Tasks.Remove(task);
        SaveChanges();
    }

    public void DeleteGroup(Guid groupId)
    {
        TaskGroup? deleteGroup = TaskGroups.Find(groupId);
        var tasks = Tasks.ToList();
        foreach (Task? task in tasks.Where(task => task?.Group != null && task.Group.Equals(deleteGroup)))
        {
            if (task != null) task.Group = null;
        }

        TaskGroups.Remove(deleteGroup);
        SaveChanges();
    }

    public void DeleteSubtask(Guid subtaskId)
    {
        Subtask? subtask = Subtasks.Find(subtaskId);
        if (subtask == null) return;
        Subtasks.Remove(subtask);
        SaveChanges();
    }

    public Task? GetTask(Guid taskId)
    {
        return Tasks.Find(taskId);
    }

    public Subtask? GetSubtask(Guid subtaskId)
    {
        return Subtasks.Find(subtaskId);
    }

    public List<TaskGroup> GetNonEmptyGroups()
    {
        HashSet<TaskGroup> set = new();
        var tasks = Tasks.ToList();
        foreach (Task? task in tasks.Where(task => task is { Group: { } }))
        {
            if (task?.Group != null)
                set.Add(task.Group);
        }

        return set.ToList();
    }
    
    IEnumerable<Task> IDatabase.Tasks()
    {
        return Tasks.ToList();
    }

    IEnumerable<Subtask> IDatabase.Subtasks()
    {
        return Subtasks.ToList();
    }

    void IDatabase.SaveChanges()
    {
        SaveChanges();
    }
}