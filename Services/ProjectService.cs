using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.DTOs;
using TaskFlow.Models;

namespace TaskFlow.Services;

public class ProjectService
{
    private readonly TaskFlowDbContext _context;

    public ProjectService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> CreateAsync(int userId, CreateProjectRequest request)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == userId);

        if (!userExists)
        {
            return null;
        }

        var project = new Project
        {
            Name = request.Name,
            UserId = userId
        };

        _context.Projects.Add(project);

        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<List<Project>> GetByUserIdAsync(int userId)
    {
        return await _context.Projects.Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(int userId, int projectId)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);

        if (project is null)
        {
            return false;
        }

        _context.Projects.Remove(project);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(int userId, int projectId, UpdateProjectRequest request)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);

        if (project is null)
        {
            return false;
        }

        project.Name = request.Name.Trim();

        await _context.SaveChangesAsync();

        return true;
    }
}