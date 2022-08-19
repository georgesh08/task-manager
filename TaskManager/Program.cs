using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManager.CLI;
using TaskManager.Database;
using TaskManager.Entities;
using TaskManager.Generators;
using TaskManager.Management;
using Task = TaskManager.Entities.Task;

namespace TaskManager;

public class Program
{
    public static void Main(string[] args)
    {
        // MultipleActiveResultSets=True
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TaskManager.Database.TaskManagerContext;";
        var optionsBuilder = new DbContextOptionsBuilder<TaskManagerContext>();
        optionsBuilder.UseSqlServer(connectionString);
        using var context = new TaskManagerContext(optionsBuilder.Options);
        var m = new Manager(context);
        var i = new ConsoleInterface(m);
        i.Launch();
    }
}

