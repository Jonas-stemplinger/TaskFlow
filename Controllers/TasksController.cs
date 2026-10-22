using Microsoft.AspNetCore.Mvc;
using TaskFlow.DTOs;
using TaskFlow.Models;
using TaskFlow.Services;

namespace TaskFlow.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TaskService _service;
    private readonly ILogger<TasksController> _logger;

    public TasksController(
        TaskService service,
        ILogger<TasksController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET /api/tasks
    [HttpGet]
    public async Task<ActionResult<List<TaskItem>>> GetAll()
    {
        var tasks = await _service.GetAllAsync();

        return Ok(tasks);
    }

    // GET /api/tasks/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await _service.GetByIdAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    // POST /api/projects/3/tasks
    [HttpPost("/api/projects/{projectId:int}/tasks")]
    public async Task<ActionResult<TaskItem>> CreateForProject(
        int projectId,
        [FromBody] CreateTaskRequest request)
    {
        var task = await _service.CreateForProjectAsync(
            projectId,
            request);

        if (task is null)
        {
            return NotFound("Projekt wurde nicht gefunden.");
        }

        _logger.LogInformation(
            "Task {TaskId} wurde für Projekt {ProjectId} erstellt.",
            task.Id,
            projectId);

        return CreatedAtAction(
            nameof(GetById),
            new { id = task.Id },
            task);
    }

    // GET /api/projects/3/tasks
    [HttpGet("/api/projects/{projectId:int}/tasks")]
    public async Task<ActionResult<List<TaskItem>>> GetByProject(
        int projectId)
    {
        var tasks = await _service.GetByProjectIdAsync(projectId);

        return Ok(tasks);
    }

    // PUT /api/tasks/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateTaskRequest request)
    {
        var updated = await _service.UpdateAsync(id, request);

        if (!updated)
        {
            return NotFound();
        }

        _logger.LogInformation(
            "Task {TaskId} wurde aktualisiert.",
            id);

        return NoContent();
    }

    // DELETE /api/tasks/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        _logger.LogInformation(
            "Task {TaskId} wurde gelöscht.",
            id);

        return NoContent();
    }

    [HttpGet("/api/teamprojects/{teamprojectId:int}/tasks")]
    public async Task<IActionResult> GetTeamTasks(
    int teamprojectId)
    {
        var tasks = await _service
            .GetByTeamprojectIdAsync(teamprojectId);

        return Ok(tasks);
    }

    [HttpPost("/api/teamprojects/{teamprojectId:int}/tasks")]
    public async Task<IActionResult> CreateTeamTask(
    int teamprojectId,
    [FromBody] CreateTaskRequest request)
    {
        var task = await _service
            .CreateForTeamprojectAsync(teamprojectId, request);

        if (task == null)
        {
            return NotFound("Teamprojekt nicht gefunden.");
        }

        return Ok(task);
    }
}