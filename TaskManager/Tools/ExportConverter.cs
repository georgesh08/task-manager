using TaskManager.Entities;
using TaskManager.Management;
using Task = TaskManager.Entities.Task;

namespace TaskManager.Tools;

public sealed class ExportConverter
{
    private static ExportConverter _instance;
    
    private ExportConverter() { }

    public static ExportConverter GetInstance()
    {
        return _instance ?? new ExportConverter();
    }

    public ExportTask ConvertToExportTask(ITaskManager manager, Task task)
    {
        var exportTask = new ExportTask(task);
        var tmp = manager.GetAllSubtasks().Where(s => s.Task != null && s.Task.UserId.Equals(task.UserId)).ToList();
        var subtasks = tmp.Select(subtask => new ExportSubtask(subtask)).ToList();
        exportTask.RelatedSubtasks = subtasks;
        return exportTask;
    }
}