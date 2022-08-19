using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Management;

public interface ITaskManager
{
    Task CreateTask(string info);
    Task GetTask(int taskId);
    IEnumerable<Task> GetAllTasks();
    void RemoveTask(int taskId);
    void CompleteTask(int taskId);
    List<Task> GetAllCompletedTasks();
    void SetTaskDeadline(int taskId, DateTime deadline);
    List<Task> GetTodayTasks();
    TaskGroup CreateNewTaskGroup(string name);
    void RemoveTaskGroup(int groupId);
    void AddTaskToGroup(int taskId, int groupId);
    void RemoveTaskFromGroup(int taskId);
    List<TaskGroup> GetGroupsWithTasks();
    Subtask CreateSubtask(string subtaskInfo);
    void DeleteSubtask(int subtaskId);
    void AttachSubtaskToTask(int subtaskId, int taskId);
    void CompleteSubtask(int subtaskId);
    List<Subtask> GetAllSubtasks();
    List<ExportTask> LoadTasks(string importPath);
    List<ExportSubtask> LoadSubtasks(string importPath);
    List<ExportGroup> LoadGGroups(string importPath);
}