using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;
using TaskManager.Exceptions;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Database;

public class TaskManagerContext : DbContext, IDatabase
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Subtask> Subtasks { get; set; }
    public DbSet<TaskGroup> TaskGroups { get; set; }
    
    public TaskManagerContext() { }
    public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options) { }

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

    public void DeleteTask(int taskId)
    {
        Task? task = GetTask(taskId);
        var subTasks = Subtasks.ToList();
        foreach (Subtask? subtask in subTasks.Where(subtask => subtask.Task != null && subtask.Task.Equals(task)))
        {
            Subtasks.Remove(subtask);
        }

        if (task == null)
        {
            throw new ObjectExistenceException("No such task in database.");
        }
        
        Tasks.Remove(task);
        SaveChanges();
    }

    public void DeleteGroup(int groupId)
    {
        TaskGroup? deleteGroup = GetGroup(groupId);
        var tasks = Tasks.ToList();
        foreach (Task? task in tasks.Where(task => task.Group != null && task.Group.Equals(deleteGroup)))
        {
            task.Group = null;
        }

        if (deleteGroup == null)
        {
            throw new ObjectExistenceException("No such group in database.");
        }

        TaskGroups.Remove(deleteGroup);
        SaveChanges();
    }

    public void DeleteSubtask(int subtaskId)
    {
        Subtask? subtask = GetSubtask(subtaskId);
        if (subtask == null)
        {
            throw new ObjectExistenceException("No such subtask in database.");
        }
        Subtasks.Remove(subtask);
        SaveChanges();
    }

    public Task? GetTask(int taskId)
    {
        return Tasks.SingleOrDefault(t => t.UserId.Equals(taskId));
    }

    public Subtask? GetSubtask(int subtaskId)
    {
        return Subtasks.SingleOrDefault(s => s.UserId.Equals(subtaskId));
    }

    public TaskGroup? GetGroup(int groupId)
    {
        return TaskGroups.SingleOrDefault(g => g.UserId.Equals(groupId));
    }

    public List<TaskGroup> GetNonEmptyGroups()
    {
        HashSet<TaskGroup> set = new();
        var tasks = Tasks.ToList();
        foreach (Task? task in tasks.Where(task => task is { Group: { } }))
        {
            if (task.Group != null)
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