using TaskManager.Database;
using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Management;

public class Manager : ITaskManager
{
    private IDatabase _database;
    private string _exportPath;

    public Manager(IDatabase databaseContext, string exportPath)
    {
        _database = databaseContext;
        _exportPath = exportPath;
    }
    
    public Task CreateNewTask(string info)
    {
        var newTask = new Task();
        newTask.UpdateInformation(info);
        _database.AddTask(newTask);
        return newTask;
    }

    public IEnumerable<Task> GetAllTasks()
    {
        return _database.Tasks();
    }

    public void RemoveTask(Guid taskId)
    {
        _database.DeleteTask(taskId);
    }

    public void CompleteTask(Guid taskId)
    {
        Task? task = _database.GetTask(taskId);
        task?.CompleteTask();
    }

    public List<Task> GetAllCompletedTasks()
    {
        return GetAllTasks().Where(task => task.IsCompleted).ToList();
    }

    public void SetTaskDeadline(Guid taskId, DateOnly deadline)
    {
        Task? task = _database.GetTask(taskId);
        task?.SetDeadline(deadline);
    }

    public List<Task> GetTodayTask()
    {
        return GetAllTasks().Where(task => task.Deadline == DateOnly.FromDateTime(DateTime.Today)).ToList();
    }

    public TaskGroup CreateNewTaskGroup(string name)
    {
        var taskGroup = new TaskGroup
        {
            Name = name
        };
        _database.AddTaskGroup(taskGroup);
        return taskGroup;
    }

    public void RemoveTaskGroup(Guid groupId)
    {
        _database.DeleteGroup(groupId);
    }

    public void AddTaskToGroup(Guid taskId, Guid groupId)
    {
        _database.AddTaskToGroup(taskId, groupId);
    }

    public void RemoveTaskFromGroup(Guid taskId)
    {
        _database.GetTask(taskId)!.Group = null;
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

    public void AttachSubtaskToTask(Guid subtaskId, Guid taskId)
    {
        Task? task = _database.GetTask(taskId);
        Subtask? subtask = _database.GetSubtask(subtaskId);
        if (task != null) subtask?.SetTask(task);
    }

    public void CompleteSubtask(Guid subtaskId)
    {
        Subtask? subtask = _database.GetSubtask(subtaskId);
        subtask?.CompleteTask();
    }

    public List<Subtask> GetAllSubtasks(Guid taskId)
    {
        return _database.Subtasks().ToList();
    }

    public void ExportData()
    {
        throw new NotImplementedException();
    }

    public void LoadData(string path)
    {
        throw new NotImplementedException();
    }
}