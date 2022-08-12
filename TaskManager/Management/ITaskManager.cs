using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Management;

public interface ITaskManager
{
    string ExportPath { get; } 
    Task CreateTask(string info);
    Task? GetTask(int taskId);
    IEnumerable<Task> GetAllTasks();
    void RemoveTask(int taskId);
    void CompleteTask(int taskId);
    List<Task> GetAllCompletedTasks();
    void SetTaskDeadline(int taskId, DateOnly deadline);
    List<Task> GetTodayTask();
    TaskGroup CreateNewTaskGroup(string name);
    void RemoveTaskGroup(int groupId);
    void AddTaskToGroup(int taskId, int groupId);
    void RemoveTaskFromGroup(int taskId);
    List<TaskGroup> GetGroupsWithTasks();
    Subtask CreateSubtask(string subtaskInfo);
    void AttachSubtaskToTask(int subtaskId, int taskId);
    void CompleteSubtask(int subtaskId);
    List<Subtask> GetAllSubtasks(int taskId);
    void ExportData();
    void LoadData(string path);
}