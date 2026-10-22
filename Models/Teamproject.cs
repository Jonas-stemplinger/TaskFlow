namespace TaskFlow.Models
{
    public class Teamproject
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsTeamProject { get; set; } = true;

        public List<AppUser> Users { get; set; } = new();

        public List<TaskItem> Tasks { get; set; } = new();
    }
}