using Microsoft.AspNetCore.Mvc;
using TaskFlow.DTOs;
using TaskFlow.Models;
using TaskFlow.Services;

namespace TaskFlow.Controllers;

[ApiController]
[Route("api/tasks/{taskId:int}/todos")]
public class TodosController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodosController(TodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(
        int taskId,
        [FromBody] CreateTodoRequest request)
    {
        var todo = await _todoService.CreateAsync(
            taskId,
            request);

        if (todo is null)
        {
            return NotFound("Task wurde nicht gefunden.");
        }

        return Created($"/api/tasks/{taskId}/todos/{todo.Id}", todo);
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoItem>>> GetByTask(int taskId)
    {
        var todos = await _todoService.GetByTaskIdAsync(taskId);

        return Ok(todos);
    }

    [HttpGet("{todoId:int}")]
    public async Task<ActionResult<TodoItem>> GetById(int taskId, int todoId)
    {
        var todo = await _todoService.GetByIdAsync(todoId);

        if (todo is null || todo.TaskItemId != taskId)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpDelete("{todoId:int}")]
    public async Task<IActionResult> Delete(int taskId, int todoId)
    {
        var deleted = await _todoService.DeleteAsync(taskId, todoId);

        if (!deleted)
        {
            return NotFound("Todo wurde nicht gefunden.");
        }

        return NoContent();
    }

    [HttpPut("{todoId:int}")]
    public async Task<IActionResult> Update(
    int taskId,
    int todoId,
    [FromBody] UpdateTodoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Todo-Titel darf nicht leer sein.");
        }

        var updated = await _todoService.UpdateAsync(
            taskId,
            todoId,
            request);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }
}