using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel;
using TaskFlow.Data;
using TaskFlow.DTOs;
using TaskFlow.Models;

namespace TaskFlow.Services;

public class DashboardService
{
    private readonly TaskFlowDbContext _context;

    public DashboardService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Dashboard> CreateDashboard(int userId, Dashboard dashboard)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == userId);

        if (!userExists) { return null!; }

        var newDashboard = new Dashboard
        {
            UserId = userId,
            ListHighPrio = dashboard.ListHighPrio,
            ListMidPrio = dashboard.ListMidPrio,
            ListLowPrio = dashboard.ListLowPrio
        };

        _context.Dashboards.Add(newDashboard);

        await _context.SaveChangesAsync();

        return newDashboard;
    }

    public async Task<bool> CheckIfUserHasADashboard(int userId)
    {
        var userHasDashboard = await _context.Dashboards.AnyAsync(u => u.UserId == userId);

        return userHasDashboard;
    }

    public async Task<Dashboard?> GetDashboardByUser(int userId)
    {
        return await _context.Dashboards
            .Include(d => d.ListHighPrio)
            .Include(d => d.ListMidPrio)
            .Include(d => d.ListLowPrio)
            .FirstOrDefaultAsync(d => d.UserId == userId);
    }

    public async Task<bool> UpdateAsync(int userId, Dashboard dashboard)
    {
        var currentDashboard = await _context.Dashboards.FirstOrDefaultAsync(d => d.UserId == userId);

        if (currentDashboard == null) { return false; }

        currentDashboard.ListMidPrio = dashboard.ListMidPrio;
        currentDashboard.ListLowPrio = dashboard.ListLowPrio;
        currentDashboard.ListHighPrio = dashboard.ListHighPrio;

        await _context.SaveChangesAsync();

        return true;
    }
}