namespace TaskFlow.DTOs
{
    public class DashboardTodoDto
    {
        public int TodoId { get; set; }
        public int TaskId { get; set; }
        public string Priority { get; set; } = "low";
        public int Position { get; set; }
    }
}
