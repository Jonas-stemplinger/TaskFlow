using Microsoft.AspNetCore.Mvc;
using TaskFlow.DTOs;
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
        var dashboard =
            await _dashboardService.GetDashboardByUser(userId);

        return Ok(dashboard);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDashboard(
        int userId,
        [FromBody] UpdateDashboardRequest request)
    {
        var updated =
            await _dashboardService.UpdateAsync(userId, request);

        if (!updated)
        {
            return BadRequest(
                "Dashboard konnte nicht gespeichert werden."
            );
        }

        return NoContent();
    }
}