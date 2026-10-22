using System.ComponentModel.DataAnnotations;
using TaskFlow.Models;

namespace TaskFlow.DTOs
{
    public class CreateTeamprojectRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public List<string> UsersListEmail { get; set; } = new List<string>();
    }
}
