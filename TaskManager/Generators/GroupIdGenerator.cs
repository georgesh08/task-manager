namespace TaskManager.Generators;

public sealed class GroupIdGenerator
{
    private static int _id;
    private static GroupIdGenerator _instance;
    
    private GroupIdGenerator() { }

    public static GroupIdGenerator GetInstance()
    {
        return _instance ??= new GroupIdGenerator();
    }

    public int GenerateId()
    {
        return ++_id;
    }
}