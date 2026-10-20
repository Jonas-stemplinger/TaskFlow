using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs;

public class UpdateProjectRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
}