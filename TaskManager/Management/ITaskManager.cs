using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Management;

public interface ITaskManager
{
    string ExportPath { get; } 
    Task CreateNewTask(string info);
    IEnumerable<Task> GetAllTasks();
    void RemoveTask(Guid taskId);
    void CompleteTask(Guid taskId);
    List<Task> GetAllCompletedTasks();
    void SetTaskDeadline(Guid taskId, DateOnly deadline);
    List<Task> GetTodayTask();
    TaskGroup CreateNewTaskGroup(string name);
    void RemoveTaskGroup(Guid groupId);
    void AddTaskToGroup(Guid taskId, Guid groupId);
    void RemoveTaskFromGroup(Guid taskId);
    List<TaskGroup> GetGroupsWithTasks();
    Subtask CreateSubtask(string subtaskInfo);
    void AttachSubtaskToTask(Guid subtaskId, Guid taskId);
    void CompleteSubtask(Guid subtaskId);
    List<Subtask> GetAllSubtasks(Guid taskId);
    void ExportData();
    void LoadData(string path);
}