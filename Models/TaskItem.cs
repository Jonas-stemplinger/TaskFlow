using TaskFlow.Models;

public class TaskItem
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    // Normales Projekt
    public int? ProjectId { get; set; }

    public Project? Project { get; set; }

    // Teamprojekt
    public int? TeamprojectId { get; set; }

    public Teamproject? Teamproject { get; set; }

    public List<TodoItem> Todos { get; set; } = new();
}