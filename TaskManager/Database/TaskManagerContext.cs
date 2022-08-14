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

    public void AddTaskToGroup(int taskId, int groupId)
    {
        TaskGroup? group = TaskGroups.SingleOrDefault(g => g != null && g.UserId.Equals(groupId));
        Task? task = Tasks.SingleOrDefault(t => t != null && t.UserId.Equals(taskId));
        if (task != null) task.Group = group;
        SaveChanges();
    }

    public void DeleteTask(int taskId)
    {
        Task? task = Tasks.SingleOrDefault(t => t != null && t.UserId.Equals(taskId));
        var subTasks = Subtasks.ToList();
        foreach (Subtask? subtask in subTasks.Where(subtask => subtask?.Task != null && subtask.Task.Equals(task)))
        {
            if (subtask != null) Subtasks.Remove(subtask);
        }

        Tasks.Remove(task);
        SaveChanges();
    }

    public void DeleteGroup(int groupId)
    {
        TaskGroup? deleteGroup = TaskGroups.SingleOrDefault(g => g != null && g.UserId.Equals(groupId));
        var tasks = Tasks.ToList();
        foreach (Task? task in tasks.Where(task => task?.Group != null && task.Group.Equals(deleteGroup)))
        {
            if (task != null) task.Group = null;
        }

        TaskGroups.Remove(deleteGroup);
        SaveChanges();
    }

    public void DeleteSubtask(int subtaskId)
    {
        Subtask? subtask = Subtasks.SingleOrDefault(s => s != null && s.UserId.Equals(subtaskId));
        if (subtask == null) return;
        Subtasks.Remove(subtask);
        SaveChanges();
    }

    public Task? GetTask(int taskId)
    {
        return Tasks.SingleOrDefault(t => t != null && t.UserId.Equals(taskId));
    }

    public Subtask? GetSubtask(int subtaskId)
    {
        return Subtasks.SingleOrDefault(s => s != null && s.UserId.Equals(subtaskId));
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