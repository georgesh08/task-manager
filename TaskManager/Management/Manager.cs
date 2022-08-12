using TaskManager.Database;
using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Management;

public class Manager : ITaskManager
{
    private IDatabase _database;
    public string ExportPath { get; }

    public Manager(IDatabase databaseContext, string exportPath)
    {
        _database = databaseContext;
        ExportPath = exportPath;
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

    public void RemoveTask(int taskId)
    {
        _database.DeleteTask(taskId);
    }

    public void CompleteTask(int taskId)
    {
        Task? task = _database.GetTask(taskId);
        task?.CompleteTask();
        _database.SaveChanges();
    }

    public List<Task> GetAllCompletedTasks()
    {
        return GetAllTasks().Where(task => task.IsCompleted).ToList();
    }

    public void SetTaskDeadline(int taskId, DateOnly deadline)
    {
        Task? task = _database.GetTask(taskId);
        task?.SetDeadline(deadline);
        _database.SaveChanges();
    }

    public List<Task> GetTodayTask()
    {
        return GetAllTasks().Where(task => task.Deadline == DateOnly.FromDateTime(DateTime.Today)).ToList();
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
        _database.AddTaskToGroup(taskId, groupId);
    }

    public void RemoveTaskFromGroup(int taskId)
    {
        Task? task = _database.GetTask(taskId);
        if (task != null) task.Group = null;
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

    public void AttachSubtaskToTask(int subtaskId, int taskId)
    {
        Task? task = _database.GetTask(taskId);
        Subtask? subtask = _database.GetSubtask(subtaskId);
        if (task != null) subtask?.SetTask(task);
        _database.SaveChanges();
    }

    public void CompleteSubtask(int subtaskId)
    {
        Subtask? subtask = _database.GetSubtask(subtaskId);
        subtask?.CompleteTask();
        _database.SaveChanges();
    }

    public List<Subtask> GetAllSubtasks(int taskId)
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