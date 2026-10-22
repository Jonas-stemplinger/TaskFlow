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

        public async Task<bool> UpdateAsync(int teamprojectId, string name)
        {
            var teamproject = await _context.Teamprojects.FindAsync(teamprojectId);

            if (teamproject == null)
            {
                return false;
            }

            teamproject.Name = name.Trim();

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int teamprojectId)
        {
            var teamproject = await _context.Teamprojects
                .FindAsync(teamprojectId);

            if (teamproject == null)
            {
                return false;
            }

            _context.Teamprojects.Remove(teamproject);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<AppUser>> GetMembersAsync(int teamprojectId)
        {
            var teamproject = await _context.Teamprojects
                .Include(t => t.Users)
                .FirstOrDefaultAsync(t => t.Id == teamprojectId);

            if (teamproject == null)
            {
                return new List<AppUser>();
            }

            return teamproject.Users;
        }

        public async Task<bool> RemoveUserAsync(int teamprojectId, int userId)
        {
            var teamproject = await _context.Teamprojects.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id == teamprojectId);

            if (teamproject == null)
            {
                return false;
            }

            var user = teamproject.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            teamproject.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsMemberAsync(int teamprojectId, int userId)
        {
            return await _context.Teamprojects
                .AnyAsync(t =>
                    t.Id == teamprojectId &&
                    t.Users.Any(u => u.Id == userId));
        }
    }
}