using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Database;

public interface IDatabase
{
    void AddTask(Task task);
    void AddSubtask(Subtask subtask);
    void AddTaskGroup(TaskGroup group);
    void AddTaskToGroup(int taskId, int groupId);
    void DeleteTask(int taskId);
    void DeleteGroup(int groupId);
    void DeleteSubtask(int subtaskId);
    Task? GetTask(int taskId);
    Subtask? GetSubtask(int subtaskId);
    List<TaskGroup> GetNonEmptyGroups();
    IEnumerable<Task> Tasks();
    IEnumerable<Subtask> Subtasks();
    void SaveChanges();
}