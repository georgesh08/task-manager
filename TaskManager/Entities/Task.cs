using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Entities;

public class Task : AbstractTask
{ 
    [Column(TypeName="datetime2")]
    public DateTime Deadline { get; private set; }
    public List<Subtask> Subtasks { get; set; } = new();
    
    public void SetDeadline(DateTime deadline)
    {
        Deadline = deadline;
    }
}