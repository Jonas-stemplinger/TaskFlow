using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    public class UpdateTaskRequest
    {
        [Required(ErrorMessage = "Ein Titel ist erforderlich.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Der Titel muss 3 bis 100 Zeichen haben.")]
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}
