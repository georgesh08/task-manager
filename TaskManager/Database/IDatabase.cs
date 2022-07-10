using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Database;

public interface IDatabase
{
    void AddTask(Task task);
    void AddSubtask(Subtask subtask);
    void AddTaskGroup(TaskGroup group);
    void AddTaskToGroup(Guid taskId, Guid groupId);
    void DeleteTask(Guid taskId);
    void DeleteGroup(Guid groupId);
    void DeleteSubtask(Guid subtaskId);
    Task? GetTask(Guid taskId);
    Subtask? GetSubtask(Guid subtaskId);
    List<TaskGroup> GetNonEmptyGroups();
    IEnumerable<Task> Tasks();
    IEnumerable<Subtask> Subtasks();
}