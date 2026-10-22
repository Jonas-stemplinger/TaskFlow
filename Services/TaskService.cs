using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.DTOs;
using TaskFlow.Models;

namespace TaskFlow.Services
{
    public class TaskService
    {
        private readonly TaskFlowDbContext _db;

        public TaskService(TaskFlowDbContext db)
        {
            _db = db;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _db.Tasks
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _db.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskItem?> CreateForProjectAsync(
            int projectId,
            CreateTaskRequest request)
        {
            var projectExists = await _db.Projects
                .AnyAsync(p => p.Id == projectId);

            if (!projectExists)
            {
                return null;
            }

            var task = new TaskItem
            {
                Title = request.Title,
                IsCompleted = request.IsCompleted,
                ProjectId = projectId
            };

            _db.Tasks.Add(task);

            await _db.SaveChangesAsync();

            return task;
        }

        public async Task<List<TaskItem>> GetByProjectIdAsync(int projectId)
        {
            return await _db.Tasks
                .AsNoTracking()
                .Where(t => t.ProjectId == projectId)
                .OrderBy(t => t.Id)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateTaskRequest request)
        {
            var task = await _db.Tasks.FindAsync(id);

            if (task is null)
            {
                return false;
            }

            task.Title = request.Title;
            task.IsCompleted = request.IsCompleted;

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _db.Tasks.FindAsync(id);

            if (task is null)
            {
                return false;
            }

            _db.Tasks.Remove(task);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<TaskItem?> CreateForTeamprojectAsync(int teamprojectId, CreateTaskRequest request)
        {
            var exists = await _db.Teamprojects
                .AnyAsync(p => p.Id == teamprojectId);

            if (!exists)
            {
                return null;
            }

            var task = new TaskItem
            {
                Title = request.Title,
                IsCompleted = request.IsCompleted,
                TeamprojectId = teamprojectId
            };

            _db.Tasks.Add(task);

            await _db.SaveChangesAsync();

            return task;
        }

        public async Task<List<TaskItem>> GetByTeamprojectIdAsync(int teamprojectId)
        {
            return await _db.Tasks
                .AsNoTracking()
                .Where(t => t.TeamprojectId == teamprojectId)
                .OrderBy(t => t.Id)
                .ToListAsync();
        }
    }
}