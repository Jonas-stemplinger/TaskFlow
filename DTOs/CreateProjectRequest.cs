using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs;

public class CreateProjectRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
}