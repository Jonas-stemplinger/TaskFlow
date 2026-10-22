namespace TaskFlow.DTOs
{
    public class UpdateDashboardRequest
    {
        public List<DashboardTodoDto> Todos { get; set; } = new();
    }
}
