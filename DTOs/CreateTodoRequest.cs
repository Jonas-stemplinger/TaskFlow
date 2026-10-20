using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs;

public class CreateTodoRequest
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}