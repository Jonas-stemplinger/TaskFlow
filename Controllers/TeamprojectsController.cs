using Microsoft.AspNetCore.Mvc;
using TaskFlow.DTOs;
using TaskFlow.Services;

namespace TaskFlow.Controllers
{
    [ApiController]
    [Route("api/users/{userId:int}/teamprojects")]
    public class TeamprojectsController : ControllerBase
    {
        private readonly TeamprojectService _teamprojectService;

        public TeamprojectsController(
            TeamprojectService teamprojectService)
        {
            _teamprojectService = teamprojectService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            int userId,
            [FromBody] CreateTeamprojectRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Projektname fehlt.");
            }

            var project = await _teamprojectService
                .CreateAsync(userId, request);

            if (project == null)
            {
                return NotFound("Benutzer nicht gefunden.");
            }

            return Ok(new
            {
                project.Id,
                project.Name,
                project.IsTeamProject
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetByUser(
            int userId)
        {
            var projects = await _teamprojectService
                .GetByUserIdAsync(userId);

            return Ok(projects.Select(p => new
            {
                p.Id,
                p.Name,
                p.IsTeamProject
            }));
        }

        [HttpPost("{teamprojectId:int}/members")]
        public async Task<IActionResult> AddMember(int userId, int teamprojectId, [FromBody] AddUserTeamprojectRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserEmail))
            {
                return BadRequest("E-Mail-Adresse fehlt.");
            }

            var result = await _teamprojectService.AddUserAsync(
                teamprojectId,
                request.UserEmail);

            return result switch
            {
                "SUCCESS" => Ok(new
                {
                    message = "Mitglied hinzugefügt."
                }),

                "PROJECT_NOT_FOUND" => NotFound("Teamprojekt nicht gefunden."),

                "USER_NOT_FOUND" => NotFound("Benutzer nicht gefunden."),

                "ALREADY_MEMBER" => Conflict("Benutzer ist bereits Mitglied."),

                _ => StatusCode(500)
            };
        }

        [HttpPut("{teamprojectId:int}")]
        public async Task<IActionResult> Update(int userId, int teamprojectId, [FromBody] UpdateTeamprojectRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Projektname fehlt.");
            }

            var updated = await _teamprojectService.UpdateAsync(teamprojectId, request.Name);

            if (!updated)
            {
                return NotFound("Teamprojekt nicht gefunden.");
            }

            return NoContent();
        }

        [HttpDelete("{teamprojectId:int}")]
        public async Task<IActionResult> Delete(int userId, int teamprojectId)
        {
            var deleted = await _teamprojectService
                .DeleteAsync(teamprojectId);

            if (!deleted)
            {
                return NotFound("Teamprojekt nicht gefunden.");
            }

            return NoContent();
        }

        [HttpGet("{teamprojectId:int}/members")]
        public async Task<IActionResult> GetMembers(int userId, int teamprojectId)
        {
            var members = await _teamprojectService
                .GetMembersAsync(teamprojectId);

            return Ok(members.Select(u => new
            {
                u.Id,
                u.Name,
                u.Email
            }));
        }

        [HttpDelete("{teamprojectId:int}/members/{memberId:int}")]
        public async Task<IActionResult> RemoveMember(int userId, int teamprojectId, int memberId)
        {
            var removed = await _teamprojectService.RemoveUserAsync(teamprojectId, memberId);

            if (!removed)
            {
                return NotFound("Teamprojekt oder Mitglied nicht gefunden.");
            }

            return NoContent();
        }
    }
}