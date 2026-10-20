using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskFlow.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        //[EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [JsonIgnore] //Passwörter dürfen auf keinem Fall irgendwie über die Api geschickt werden
        public string Password { get; set; } = string.Empty;    

        public List<Project> Projects { get; set; } = new();
    }
}