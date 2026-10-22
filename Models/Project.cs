using System.Reflection.Metadata.Ecma335;

namespace TaskFlow.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public List<TaskItem> Tasks { get; set; } = new();
        public bool isTeamProject { get; set; } = false;
    }
}
