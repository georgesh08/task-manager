namespace TaskManager.Entities;

public class Subtask : AbstractTask
{
    public Task? Task { get; private set; }

    public void SetTask(Task task)
    {
        Task = task;
    }
}