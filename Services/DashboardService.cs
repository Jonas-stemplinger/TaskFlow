using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.DTOs;
using TaskFlow.Models;

namespace TaskFlow.Services;

public class DashboardService
{
    private readonly TaskFlowDbContext _context;

    public DashboardService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<List<DashboardTodoDto>> GetDashboardByUser(int userId)
    {
        return await _context.DashboardTodos
            .AsNoTracking()
            .Where(t => t.Dashboard.UserId == userId)
            .OrderBy(t => t.Position)
            .Select(t => new DashboardTodoDto
            {
                TodoId = t.TodoId,
                TaskId = t.Todo.TaskItemId,
                Priority = t.Priority,
                Position = t.Position
            })
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(
        int userId,
        UpdateDashboardRequest request)
    {
        var userExists = await _context.Users
            .AnyAsync(u => u.Id == userId);

        if (!userExists)
        {
            return false;
        }

        var validPriorities = new[] { "low", "medium", "high" };

        if (request.Todos == null ||
            request.Todos.Any(t =>
                !validPriorities.Contains(t.Priority) ||
                t.Position < 0))
        {
            return false;
        }

        if (request.Todos.Select(t => t.TodoId).Distinct().Count()
            != request.Todos.Count)
        {
            return false;
        }

        var todoIds = request.Todos.Select(t => t.TodoId).ToList();

        var validTodos = await _context.Todos.Where(t => todoIds.Contains(t.Id) && t.TaskItem.Project.UserId == userId)
            .Select(t => new
            {
                t.Id,
                t.TaskItemId
            })
            .ToListAsync();

        if (validTodos.Count != todoIds.Count)
        {
            return false;
        }

        foreach (var item in request.Todos)
        {
            if (!validTodos.Any(t => t.Id == item.TodoId && t.TaskItemId == item.TaskId))
            {
                return false;
            }
        }

        var dashboard = await _context.Dashboards
            .Include(d => d.Todos)
            .FirstOrDefaultAsync(d => d.UserId == userId);

        if (dashboard == null)
        {
            dashboard = new Dashboard
            {
                UserId = userId
            };

            _context.Dashboards.Add(dashboard);
        }

        _context.DashboardTodos.RemoveRange(dashboard.Todos);

        foreach (var item in request.Todos)
        {
            dashboard.Todos.Add(new TaskModelDashboard
            {
                TodoId = item.TodoId,
                Priority = item.Priority,
                Position = item.Position
            });
        }

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteDashboardTodoAsync(int userId, int todoId)
    {
        var dashboardTodo = await _context.DashboardTodos.FirstOrDefaultAsync(dt => dt.Dashboard.UserId == userId && dt.TodoId == todoId);

        if (dashboardTodo == null)
        {
            return false;
        }

        _context.DashboardTodos.Remove(dashboardTodo);

        await _context.SaveChangesAsync();

        return true;
    }

}