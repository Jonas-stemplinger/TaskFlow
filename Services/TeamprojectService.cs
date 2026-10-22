using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.DTOs;
using TaskFlow.Models;

namespace TaskFlow.Services
{
    public class TeamprojectService
    {
        private readonly TaskFlowDbContext _context;

        public TeamprojectService(TaskFlowDbContext context)
        {
            _context = context;
        }

        public async Task<Teamproject?> CreateAsync(
            int userId,
            CreateTeamprojectRequest request)
        {
            var creator = await _context.Users
                .FindAsync(userId);

            if (creator == null)
            {
                return null;
            }

            var emails = request.UsersListEmail
                .Select(e => e.Trim().ToLower())
                .Distinct()
                .ToList();

            var users = await _context.Users
                .Where(u => emails.Contains(u.Email.ToLower()))
                .ToListAsync();

            if (!users.Any(u => u.Id == userId))
            {
                users.Add(creator);
            }

            var teamproject = new Teamproject
            {
                Name = request.Name.Trim(),
                IsTeamProject = true,
                Users = users
            };

            _context.Teamprojects.Add(teamproject);

            await _context.SaveChangesAsync();

            return teamproject;
        }

        public async Task<List<Teamproject>> GetByUserIdAsync(
            int userId)
        {
            return await _context.Teamprojects
                .Where(t => t.Users.Any(u => u.Id == userId))
                .ToListAsync();
        }

        public async Task<string> AddUserAsync(int teamprojectId, string email)
        {
            var teamproject = await _context.Teamprojects.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id == teamprojectId);

            if (teamproject == null)
            {
                return "PROJECT_NOT_FOUND";
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.Trim().ToLower());

            if (user == null)
            {
                return "USER_NOT_FOUND";
            }

            if (teamproject.Users.Any(u => u.Id == user.Id))
            {
                return "ALREADY_MEMBER";
            }

            teamproject.Users.Add(user);

            await _context.SaveChangesAsync();

            return "SUCCESS";
        }
    }
}