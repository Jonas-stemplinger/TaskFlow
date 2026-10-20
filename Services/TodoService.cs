using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.DTOs;
using TaskFlow.Models;

namespace TaskFlow.Services;

public class TodoService
{
    private readonly TaskFlowDbContext _context;

    public TodoService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<TodoItem?> CreateAsync(
        int taskId,
        CreateTodoRequest request)
    {
        var taskExists = await _context.Tasks
            .AnyAsync(t => t.Id == taskId);

        if (!taskExists)
        {
            return null;
        }

        var todo = new TodoItem
        {
            Title = request.Title,
            IsCompleted = request.IsCompleted,
            TaskItemId = taskId
        };

        _context.Todos.Add(todo);

        await _context.SaveChangesAsync();

        return todo;
    }

    public async Task<List<TodoItem>> GetByTaskIdAsync(int taskId)
    {
        return await _context.Todos.Where(t => t.TaskItemId == taskId).ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(int taskId, int todoId)
    {
        var todo = await _context.Todos
            .FirstOrDefaultAsync(t =>
                t.Id == todoId &&
                t.TaskItemId == taskId);

        if (todo is null)
        {
            return false;
        }

        _context.Todos.Remove(todo);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(int taskId, int todoId, UpdateTodoRequest request)
    {
        var todo = await _context.Todos
            .FirstOrDefaultAsync(t =>
                t.Id == todoId &&
                t.TaskItemId == taskId);

        if (todo is null)
        {
            return false;
        }

        todo.Title = request.Title.Trim();
        todo.IsCompleted = request.IsCompleted;

        await _context.SaveChangesAsync();

        return true;
    }
}