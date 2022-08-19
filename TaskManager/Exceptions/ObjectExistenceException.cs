namespace TaskManager.Exceptions;

public class ObjectExistenceException : TaskManagerException
{
    public ObjectExistenceException()
    {
    }

    public ObjectExistenceException(string message)
        : base(message)
    {
    }

    public ObjectExistenceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}