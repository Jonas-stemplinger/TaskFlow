using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    public class UpdateTeamprojectRequest
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;
    }
}