using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Models
{
    public class Dashboard
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public List<TaskModelDashboard> ListLowPrio { get; set; } = new();
        public List<TaskModelDashboard> ListMidPrio { get; set; } = new();
        public List<TaskModelDashboard> ListHighPrio { get; set; } = new();
    }
}
