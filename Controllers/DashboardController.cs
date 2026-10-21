using Microsoft.AspNetCore.Mvc;
using TaskFlow.Models;
using TaskFlow.Services;

namespace TaskFlow.Controllers;

[ApiController]
[Route("api/users/{userId:int}/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly DashboardService _dashboardService;

    public DashboardController(DashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboard(int userId)
    {
        var dashboard = await _dashboardService.GetDashboardByUser(userId);

        if (dashboard == null)
        {
            return NotFound("Dashboard nicht gefunden.");
        }

        return Ok(dashboard);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDashboard(
        int userId,
        [FromBody] Dashboard dashboard)
    {
        dashboard.UserId = userId;

        var createdDashboard =
            await _dashboardService.CreateDashboard(userId, dashboard);

        if (createdDashboard == null)
        {
            return BadRequest("Dashboard konnte nicht erstellt werden.");
        }

        return Ok(createdDashboard);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDashboard(
        int userId,
        [FromBody] Dashboard dashboard)
    {
        var updated = await _dashboardService.UpdateAsync(
            userId,
            dashboard
        );

        if (!updated)
        {
            return NotFound("Dashboard nicht gefunden.");
        }

        return NoContent();
    }
}