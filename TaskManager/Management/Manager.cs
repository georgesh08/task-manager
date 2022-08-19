using TaskManager.Database;
using TaskManager.Entities;
using TaskManager.Exceptions;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Management;

public class Manager : ITaskManager
{
    private IDatabase _database;

    public Manager(IDatabase databaseContext)
    {
        _database = databaseContext;
    }
    
    public Task CreateTask(string info)
    {
        var newTask = new Task();
        newTask.UpdateInformation(info);
        _database.AddTask(newTask);
        return newTask;
    }

    public Task GetTask(int taskId)
    {
        Task? task = _database.GetTask(taskId);
        if (task == null)
        {
            throw new ObjectExistenceException("No such object in database.");
        }

        return task;
    }

    public IEnumerable<Task> GetAllTasks()
    {
        return _database.Tasks();
    }

    public void RemoveTask(int taskId)
    {
        _database.DeleteTask(taskId);
    }

    public void CompleteTask(int taskId)
    {
        Task? task = _database.GetTask(taskId);
        if (task == null)
        {
            throw new ObjectExistenceException("No such task in database.");
        }
        task.CompleteTask();
        _database.SaveChanges();
    }

    public List<Task> GetAllCompletedTasks()
    {
        return GetAllTasks().Where(task => task.IsCompleted).ToList();
    }

    public void SetTaskDeadline(int taskId, DateTime deadline)
    {
        Task? task = _database.GetTask(taskId);
        if (task == null)
        {
            throw new ObjectExistenceException("No such task in database.");
        }
        task.SetDeadline(deadline);
        _database.SaveChanges();
    }

    public List<Task> GetTodayTasks()
    {
        return GetAllTasks().Where(task => task.Deadline == DateTime.Today).ToList();
    }

    public TaskGroup CreateNewTaskGroup(string name)
    {
        var taskGroup = new TaskGroup(name);
        _database.AddTaskGroup(taskGroup);
        return taskGroup;
    }

    public void RemoveTaskGroup(int groupId)
    {
        _database.DeleteGroup(groupId);
    }

    public void AddTaskToGroup(int taskId, int groupId)
    {
        Task? task = _database.GetTask(taskId);
        TaskGroup? group = _database.GetGroup(groupId);
        if (task == null || group == null)
        {
            throw new ObjectExistenceException("No such entity in database.");
        }

        task.Group = group;
        _database.SaveChanges();
    }

    public void RemoveTaskFromGroup(int taskId)
    {
        Task? task = _database.GetTask(taskId);
        if (task == null)
        {
            throw new ObjectExistenceException("No such task in database.");
        }
        task.Group = null;
        _database.SaveChanges();
    }

    public List<TaskGroup> GetGroupsWithTasks()
    {
        return _database.GetNonEmptyGroups();
    }

    public Subtask CreateSubtask(string subtaskInfo)
    {
        var subtask = new Subtask();
        subtask.UpdateInformation(subtaskInfo);
        _database.AddSubtask(subtask);
        return subtask;
    }

    public void DeleteSubtask(int subtaskId)
    {
        _database.DeleteSubtask(subtaskId);
    }

    public void AttachSubtaskToTask(int subtaskId, int taskId)
    {
        Task? task = _database.GetTask(taskId);
        Subtask? subtask = _database.GetSubtask(subtaskId);
        if (task == null || subtask == null)
        {
            throw new ObjectExistenceException("No such entity in database.");
        }

        subtask.SetTask(task);
        _database.SaveChanges();
    }

    public void CompleteSubtask(int subtaskId)
    {
        Subtask? subtask = _database.GetSubtask(subtaskId);
        if (subtask == null)
        {
            throw new ObjectExistenceException("No such subtask in database.");
        }
        subtask.CompleteTask();
        _database.SaveChanges();
    }

    public List<Subtask> GetAllSubtasks()
    {
        return _database.Subtasks().ToList();
    }

    public List<ExportTask> LoadTasks(string importPath)
    {
        throw new NotImplementedException();
    }

    public List<ExportSubtask> LoadSubtasks(string importPath)
    {
        throw new NotImplementedException();
    }

    public List<ExportGroup> LoadGGroups(string importPath)
    {
        throw new NotImplementedException();
    }
}