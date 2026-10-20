using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs;

public class UpdateTodoRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}