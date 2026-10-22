using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    public class AddUserTeamprojectRequest
    {
        [Required]
        public string UserEmail { get; set; } = string.Empty;
    }
}
