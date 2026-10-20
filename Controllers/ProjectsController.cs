using Microsoft.AspNetCore.Mvc;
using TaskFlow.DTOs;
using TaskFlow.Models;
using TaskFlow.Services;

namespace TaskFlow.Controllers;

[ApiController]
[Route("api/users/{userId:int}/projects")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectService _projectService;

    public ProjectsController(ProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<ActionResult<Project>> Create(int userId, [FromBody] CreateProjectRequest request)
    {
        var project = await _projectService.CreateAsync(userId, request);

        if (project is null)
        {
            return NotFound("User wurde nicht gefunden.");
        }

        return Created($"/api/users/{userId}/projects/{project.Id}", project);
    }

    [HttpGet]
    public async Task<ActionResult<List<Project>>> GetByUser(int userId)
    {
        var projects = await _projectService.GetByUserIdAsync(userId);

        return Ok(projects);
    }

    [HttpGet("{projectId:int}")]
    public async Task<ActionResult<Project>> GetById(int userId, int projectId)
    {
        var project = await _projectService.GetByIdAsync(projectId);

        if (project is null || project.UserId != userId)
        {
            return NotFound();
        }

        return Ok(project);
    }
    [HttpDelete("{projectId:int}")]
    public async Task<IActionResult> Delete(
    int userId,
    int projectId)
    {
        var deleted = await _projectService.DeleteAsync(
            userId,
            projectId);

        if (!deleted)
        {
            return NotFound("Projekt wurde nicht gefunden.");
        }

        return NoContent();
    }

    [HttpPut("{projectId:int}")]
    public async Task<IActionResult> Update(
    int userId,
    int projectId,
    [FromBody] UpdateProjectRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Projektname darf nicht leer sein.");
        }

        var updated = await _projectService.UpdateAsync(userId, projectId, request);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }
}