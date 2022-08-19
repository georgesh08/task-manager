using TaskManager.Entities;
using Task = TaskManager.Entities.Task;
using Threading = System.Threading;

namespace TaskManager.Database;

public interface IDatabase
{
    void AddTask(Task task);
    void AddSubtask(Subtask subtask);
    void AddTaskGroup(TaskGroup group);
    void DeleteTask(int taskId);
    void DeleteGroup(int groupId);
    void DeleteSubtask(int subtaskId);
    Task? GetTask(int taskId);
    Subtask? GetSubtask(int subtaskId);
    TaskGroup? GetGroup(int groupId);
    List<TaskGroup> GetNonEmptyGroups();
    IEnumerable<Task> Tasks();
    IEnumerable<Subtask> Subtasks();
    void SaveChanges();
}