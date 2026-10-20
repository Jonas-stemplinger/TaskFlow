using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs;

public class CreateUserRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}