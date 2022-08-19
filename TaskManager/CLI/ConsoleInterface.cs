using Newtonsoft.Json;
using TaskManager.Management;
using Serilog;
using Serilog.Core;
using TaskManager.Entities;
using TaskManager.Exceptions;
using TaskManager.Tools;
using Task = TaskManager.Entities.Task;

namespace TaskManager.CLI;

/// <summary>
///   Available commands:
///   --create-task {task_info} - creates new task. Information must be inside curly braces.
///   --delete-task {task_id} - deletes task by ID
///   --get-task {task_id} - returns task's full information
///   --complete-task {task_id} - completes task
///   --list-tasks - returns information about every task in database
///   --get-completed-tasks - returns every task, where field IsCompleted equals True
///   --set-deadline {task_id} {deadline_date} - sets deadline to the task
///   --today-tasks - returns information about every task, where deadline day is today
///   --create-group {group_name} - creates new task group. Group name mustn't contain spaces.
///   --delete-group {group_id} - deletes group by ID
///   --add-task-to-group {task_id} {group_id} - adds task to group
///   --remove-task-from-group {task_id} - removes task from group
///   --get-nonempty-groups - returns every not empty group (group with >= 1 task)
///   --create-subtask {subtask_info} - creates new subtask. Information must be inside curly braces.
///   --delete-subtask {subtask_id} - deletes subtask by ID
///   --link-subtask {subtask_id} {task_id} - attaches subtask to task
///   --complete-subtask {subtask_id} - completes subtask
///   --list-subtasks - returns information about every subtask in database
///   --export - exports data from data base to the folder path, given in TaskManager params
///   --import {path} - imports data from the given folder
///   --exit - stops current session
/// </summary>


public class ConsoleInterface : IConsoleInterface
{
    private readonly ITaskManager _manager;
    private readonly Logger _logger;
    private const int CommandFlagPos = 2;
    private int _numberOfParams;

    public ConsoleInterface(ITaskManager manager, string logPath = "")
    {
        _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logPath)
                .CreateLogger();
        _manager = manager;
    }
    
    public void Launch()
    {
        _logger.Information("Task manager launched successfully");
        while (true)
        {
            Console.WriteLine(console.RequestMessage);
            string? command = Console.ReadLine();
            if (command == null) return;
            if (command[..2] != "--")
            {
                _logger.Error("Invalid request format");
                continue;
            }

            if (command == "--exit")
            {
                _logger.Information("Session ended");
                break;
            }

            string operation = GetOperation(command);

            switch (operation)
            {
                case "create-task":
                    string taskInfo = ExtractInfo(command);
                    CreateTask(taskInfo);
                    break;
                case "create-subtask":
                    string subtaskInfo = ExtractInfo(command);
                    CreateSubtask(subtaskInfo);
                    break;
                default:
                {
                    string[] param = command.Split(' ');
                    _numberOfParams = param.Length - 1;

                    switch (operation)
                    {
                        case "delete-task":
                            DeleteTask(int.Parse(param[1]));
                            break;

                        case "get-task":
                            GetTask(int.Parse(param[1]));
                            break;

                        case "complete-task":
                            CompleteTask(int.Parse(param[1]));
                            break;
                        
                        case "delete-subtask":
                            DeleteSubtask(int.Parse(param[1]));
                            break;

                        case "list-tasks":
                            GetAllTasks();
                            break;

                        case "get-completed-tasks":
                            GetCompletedTasks();
                            break;

                        case "set-deadline":
                            SetDeadline(int.Parse(param[1]), DateTime.Parse(param[1]));
                            break;

                        case "today-tasks":
                            GetTodayTasks();
                            break;

                        case "create-group":
                            CreateGroup(param[1]);
                            break;

                        case "delete-group":
                            DeleteGroup(int.Parse(param[1]));
                            break;

                        case "add-task-to-group":
                            AddTaskToGroup(int.Parse(param[1]), int.Parse(param[2]));
                            break;

                        case "remove-task-from-group":
                            RemoveTaskFromGroup(int.Parse(param[1]));
                            break;

                        case "get-nonempty-groups":
                            // TODO
                            break;

                        case "link-subtask":
                            LinkSubtask(int.Parse(param[1]), int.Parse(param[2]));
                            break;

                        case "complete-subtask":
                            CompleteSubtask(int.Parse(param[1]));
                            break;

                        case "list-subtasks":
                            GetAllSubtasks();
                            break;

                        case "export":
                            ExportData();
                            break;

                        case "import":
                            ImportData(param[1]);
                            break;

                        default:
                            _logger.Error("Unknown command");
                            break;
                    }

                    break;
                }
            }
        }
    }

    private string ExtractInfo(string command)
    {
        int start = command.IndexOf('{') + 1;
        int end = command.Length - 1;
        return command.Substring(start, end - start);
    }

    private void DeleteSubtask(int id)
    {
        if (!IsCorrectNumberOfParams(1, _numberOfParams))
        {
            return;
        }

        try
        {
            _manager.DeleteSubtask(id);
        }
        catch (TaskManagerException e)
        {
            _logger.Error(e, "Operation failed.");
            return;
        }
        _logger.Information("Task {Id} deleted.", id);
    }

    private void GetCompletedTasks()
    {
        if (!IsCorrectNumberOfParams(0, _numberOfParams))
        {
            return;
        }

        List<Task> completedTasks = _manager.GetAllCompletedTasks();
        var tasks = completedTasks.Select(task => 
            ExportConverter.GetInstance().ConvertToExportTask(_manager, task))
            .ToList();

        foreach (string result in tasks.Select(exportTask => JsonConvert.SerializeObject(exportTask, Formatting.Indented)))
        {
            Console.WriteLine(result);
        }
    }
    
    private void GetAllTasks()
    {
        if (!IsCorrectNumberOfParams(0, _numberOfParams))
        {
            return;
        }

        var tasks = _manager.GetAllTasks().Select(task => 
            ExportConverter.GetInstance().ConvertToExportTask(_manager, task))
            .ToList();

        foreach (string result in tasks.Select(exportTask => JsonConvert.SerializeObject(exportTask, Formatting.Indented)))
        {
            Console.WriteLine(result);
        }
    }

    private void GetNonEmptyGroups()
    {
        if (!IsCorrectNumberOfParams(0, _numberOfParams))
        {
            return;
        }
        
        
    }

    private void GetTodayTasks()
    {
        if (!IsCorrectNumberOfParams(0, _numberOfParams))
        {
            return;
        }
        
        var tasks = _manager.GetTodayTasks().Select(task => 
            ExportConverter.GetInstance().ConvertToExportTask(_manager, task))
            .ToList();
        
        foreach (string result in tasks.Select(exportTask => JsonConvert.SerializeObject(exportTask, Formatting.Indented)))
        {
            Console.WriteLine(result);
        }
    }
    
    private void GetAllSubtasks()
    {
        if (!IsCorrectNumberOfParams(0, _numberOfParams))
        {
            return;
        }

        var subtasks = _manager.GetAllSubtasks().Select(subtask => new ExportSubtask(subtask)).ToList();
        foreach (string result in subtasks.Select(exportSubtask => 
                     JsonConvert.SerializeObject(exportSubtask, Formatting.Indented)))
        {
            Console.WriteLine(result);
        }
    }

    private void CompleteTask(int taskId)
    {
        if (!IsCorrectNumberOfParams(1, _numberOfParams))
        {
            return;
        }

        try
        {
            _manager.CompleteTask(taskId);
        }
        catch (TaskManagerException e)
        {
            _logger.Error(e, "Operation failed.");
            return;
        }
        
        _logger.Information("Task {TaskId} completed.", taskId);
    }

    private void RemoveTaskFromGroup(int taskId)
    {
        if (!IsCorrectNumberOfParams(1, _numberOfParams))
        {
           return;
        }

        try
        {
            _manager.RemoveTaskFromGroup(taskId);
        }
        catch (TaskManagerException e)
        {
            _logger.Error(e, "Operation failed.");
            return;
        }
        
        _logger.Information("Removed task {TaskId} from group.", taskId);
    }

    private void AddTaskToGroup(int taskId, int groupId)
    {
        if (!IsCorrectNumberOfParams(2, _numberOfParams))
        {
            return;
        }

        try
        {
            _manager.AddTaskToGroup(taskId, groupId);
        }
        catch (TaskManagerException e)
        {
            _logger.Error(e, "Operation failed.");
            return;
        }
        
        _logger.Information("Added task {TaskId} to group {GroupId}.", taskId, groupId);
    }

    private void CreateSubtask(string info)
    {
        Subtask subtask = _manager.CreateSubtask(info);
        _logger.Information("Subtask {Id} created.", subtask.UserId);
    }

    private void ExportData()
    {
        if (!IsCorrectNumberOfParams(0, _numberOfParams))
        {
            return;
        }
        
        // TODO
    }

    private void ImportData(string path)
    {
        if (!IsCorrectNumberOfParams(1, _numberOfParams))
        {
            return;
        }
        // TODO
    }

    private void LinkSubtask(int subtaskId, int taskId)
    {
        if (!IsCorrectNumberOfParams(2, _numberOfParams))
        {
            return;
        }

        try
        {
            _manager.AttachSubtaskToTask(subtaskId, taskId);
        }
        catch (TaskManagerException e)
        {
            _logger.Error(e, "Operation failed.");
            return;
        }
        
        _logger.Information("Linked subtask {SubtaskId} and task {TaskId}.", subtaskId, taskId);
    }

    private void DeleteGroup(int groupId)
    {
        if (!IsCorrectNumberOfParams(1, _numberOfParams))
        {
            return;
        }

        try
        {
            _manager.RemoveTaskGroup(groupId);
        }
        catch (TaskManagerException e)
        {
            _logger.Error(e, "Operation failed.");
            return;
        }
        
        _logger.Information("{Id} group deleted.", groupId);
    }

    private void CreateGroup(string name)
    {
        if (!IsCorrectNumberOfParams(1, _numberOfParams))
        {
            return;
        }
        
        TaskGroup group = _manager.CreateNewTaskGroup(name);
        _logger.Information("{Name} group created with ID {Id}.", group.Name, group.UserId);
    }

    private void CompleteSubtask(int subtaskId)
    {
        if (!IsCorrectNumberOfParams(1, _numberOfParams))
        {
            return;
        }
        try
        {
            _manager.CompleteSubtask(subtaskId);
        }
        catch (TaskManagerException e)
        {
            _logger.Error(e, "Operation failed.");
            return;
        }
        
        _logger.Information("Subtask {Id} completed.", subtaskId);
    }

    private void CreateTask(string info)
    {
        Task task = _manager.CreateTask(info);
        _logger.Information("Task {Id} created.", task.UserId);
    }

    private void DeleteTask(int taskId)
    {
        if (!IsCorrectNumberOfParams(1, _numberOfParams))
        {
            return;
        }
        try
        {
            _manager.RemoveTask(taskId);
        }
        catch (TaskManagerException e)
        {
            _logger.Error(e, "Operation failed.");
            return;
        }
        
        _logger.Information("Task {Id} deleted.", taskId);
    }

    private void GetTask(int taskId)
    {
        if (!IsCorrectNumberOfParams(1, _numberOfParams))
        {
            return;
        }
        
        try
        {
            Task task = _manager.GetTask(taskId);
            var data = new ExportTask(task);
            string export = JsonConvert.SerializeObject(data, Formatting.Indented);
            Console.WriteLine(export);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Operation failed.");
        }
    }

    private void SetDeadline(int taskId, DateTime deadline)
    {
        if (!IsCorrectNumberOfParams(2, _numberOfParams))
        {
            return;
        }
        try
        {
            _manager.SetTaskDeadline(taskId, deadline);
        }
        catch(TaskManagerException e)
        {
            _logger.Error(e, "Operation failed.");
            return;
        }
        
        _logger.Information("{Deadline} was set as deadline for {Id}.", deadline, taskId);
    }

    private bool IsCorrectNumberOfParams(int expectedNumber, int paramsSize)
    {
        if (expectedNumber == paramsSize) return true;
        _logger.Error("Wrong number of params.");
        return false;
    }
    
    private string GetOperation(string command)
    {
        int spacePos = command.IndexOf(' ');
        return spacePos == -1 ? command[CommandFlagPos..] 
            : command.Substring(CommandFlagPos, spacePos - CommandFlagPos);
    }
}