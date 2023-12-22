using ExampleProject.Models;

namespace ExampleProject;

public class TaskRepository
{
    private int idCounter = 0;

    public List<TaskEntity> Tasks { get; set; }

    public TaskRepository()
    {
        Tasks = new List<TaskEntity>()
        {
            new() { Id = GetNextId(), Owner="system", Text ="system task - initialization", DateTime = DateTime.Now },
            new() { Id = GetNextId(), Owner="system", Text ="system task - background service", DateTime = DateTime.Now },
        };
    }

    public int GetNextId() => idCounter++;
}
