namespace TaskManager.Generators;

public sealed class TaskIdGenerator
{
    private static int _id = 1000;
    private static TaskIdGenerator _instance;
    
    private TaskIdGenerator() { }

    public static TaskIdGenerator GetInstance()
    {
        return _instance ??= new TaskIdGenerator();
    }

    public int GenerateId()
    {
        return ++_id;
    }
}