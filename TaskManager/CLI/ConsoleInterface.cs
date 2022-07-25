using TaskManager.Management;
using Serilog;
using Serilog.Core;
using TaskManager.Entities;
using Task = TaskManager.Entities.Task;

namespace TaskManager.CLI;

/// <summary>
///   Available commands:
///   --create-task {task_info} - creates new task
///   --delete-task {task_id} - deletes task by ID
///   --get-task {task_id} - returns task's full information
///   --complete-task {task_id} - completes task
///   --list-tasks - returns information about every task in database
///   --get-completed-tasks - returns every task, where field IsCompleted equals True
///   --set-deadline {task_id} {deadline_date} - sets deadline to the task
///   --today-tasks - returns information about every task, where deadline day is today
///   --create-group {group_name} - creates new task group
///   --delete-group {group_id} - deletes group by ID
///   --add-task-to-group {task_id} {group_id} - adds task to group
///   --remove-task-from-group {task_id} {group_id} - removes task from group
///   --get-nonempty-groups - returns every not empty group (group with >= 1 task)
///   --create-subtask {subtask_info} - creates new subtask
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
            string[] param = command.Split(' ');
            int numberOfParams = param.Length;
            
            switch (operation)
            {
                case "create-task":
                    if (!IsCorrectNumberOfParams(1, numberOfParams))
                    {
                        break;
                    }
                    Task task = _manager.CreateNewTask(param[1]);
                    _logger.Information("Task {Id} created", task.Id);
                    break;
                
                case "delete-task":
                    if (!IsCorrectNumberOfParams(1, numberOfParams))
                    {
                        break;
                    }
                    _manager.RemoveTask(Guid.Parse(param[1]));
                    _logger.Information("Task {Id} deleted", param[1]);
                    break;
                
                case "get-task":
                    // TODO
                    break;
                
                case "complete-task":
                    // TODO
                    break;
                
                case "list-tasks":
                    // TODO
                    break;
                
                case "get-completed-tasks":
                    // TODO
                    break;
                
                case "set-deadline":
                    if (!IsCorrectNumberOfParams(2, numberOfParams))
                    {
                        break;
                    }
                    _manager.SetTaskDeadline(Guid.Parse(param[1]), DateOnly.Parse(param[2]));
                    _logger.Information("{Deadline} was set as deadline for {Id}", param[2], param[1]);
                    break;
                
                case "today-tasks":
                    // TODO
                    break;
                
                case "create-group":
                    if (!IsCorrectNumberOfParams(1, numberOfParams))
                    {
                        break;
                    }
                    TaskGroup group = _manager.CreateNewTaskGroup(param[1]);
                    _logger.Information("{Name} group created with ID {Id}", group.Name, group.Id);
                    break;
                
                case "delete-group":
                    if (!IsCorrectNumberOfParams(1, numberOfParams))
                    {
                        break;
                    }
                    _manager.RemoveTaskGroup(Guid.Parse(param[1]));
                    _logger.Information("{Id} group deleted", param[1]);
                    break;
                
                case "add-task-to-group":
                    if (!IsCorrectNumberOfParams(2, numberOfParams))
                    {
                        break;
                    }
                    _manager.AddTaskToGroup(Guid.Parse(param[1]), Guid.Parse(param[2]));
                    _logger.Information("Added task {TaskId} to group {GroupId}", param[1], param[2]);
                    break;
                
                case "remove-task-from-group":
                    if (!IsCorrectNumberOfParams(1, numberOfParams))
                    {
                        break;
                    }
                    _manager.RemoveTaskFromGroup(Guid.Parse(param[1]));
                    _logger.Information("Removed task {TaskId} from group", param[1]);
                    break;
                
                case "get-nonempty-groups":
                    // TODO
                    break;
                
                case "create-subtask":
                    if (!IsCorrectNumberOfParams(1, numberOfParams))
                    {
                        break;
                    }
                    Subtask subtask = _manager.CreateSubtask(param[1]);
                    _logger.Information("Subtask {Id} created", subtask.Id);
                    break;
                
                case "link-subtask":
                    if (!IsCorrectNumberOfParams(2, numberOfParams))
                    {
                        break;
                    }
                    _manager.AttachSubtaskToTask(Guid.Parse(param[1]), Guid.Parse(param[2]));
                    _logger.Information("Linked subtask {SubtaskId} and task {TaskId}", param[1], param[2]);
                    break;
                
                case "complete-subtask":
                    _manager.CompleteSubtask(Guid.Parse(param[1]));
                    _logger.Information("Subtask {Id} deleted", param[1]);
                    break;
                
                case "list-subtasks":
                    // TODO
                    break;
                
                case "export":
                    if (!IsCorrectNumberOfParams(0, numberOfParams))
                    {
                        break;
                    }
                    _manager.ExportData();
                    _logger.Information("Data exported to {Path}", _manager.ExportPath);
                    break;
                
                case "import":
                    if (!IsCorrectNumberOfParams(1, numberOfParams))
                    {
                        break;
                    }
                    _manager.LoadData(param[1]);
                    _logger.Information("Data imported to {Path}", param[1]);
                    break;
                
                default:
                    _logger.Error("Unknown command");
                    break;
            }
        }
    }

    private bool IsCorrectNumberOfParams(int expectedNumber, int paramsSize)
    {
        if (expectedNumber == paramsSize) return true;
        _logger.Error("Wrong number of params");
        return false;

    }
    private string GetOperation(string command)
    {
        int spacePos = command.IndexOf(' ');
        return spacePos == -1 ? command[CommandFlagPos..] 
            : command.Substring(CommandFlagPos, spacePos - CommandFlagPos);
    }
}