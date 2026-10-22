using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Models
{
    public class Dashboard
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public List<TaskModelDashboard> Todos { get; set; } = new();
    }
}
