namespace TaskFlow.Models
{
    public class TaskModelDashboard
    {
        public int Id { get; set; }

        public int DashboardId { get; set; }

        public Dashboard Dashboard { get; set; } = null!;

        public int TodoId { get; set; }

        public TodoItem Todo { get; set; } = null!;

        public string Priority { get; set; } = "low";

        public int Position { get; set; }
    }
}
