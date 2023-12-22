namespace ExampleProject.Models;

public class TaskEntity
{
    public int Id { get; set; }
    public string? Owner { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
}
