using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.DTOs;
using TaskFlow.Models;
using Microsoft.AspNetCore.Identity;

namespace TaskFlow.Services;

public class UserService
{
    private readonly TaskFlowDbContext _context;
    private readonly IPasswordHasher<AppUser> _passwordHasher;

    public UserService(TaskFlowDbContext context, IPasswordHasher<AppUser> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<AppUser> CreateAsync(CreateUserRequest request)
    {
        var user = new AppUser
        {
            Name = request.Name,
            Email = request.Email.Trim().ToLowerInvariant()
        };

        // Passwort hashen
        user.Password = _passwordHasher.HashPassword(user, request.Password);

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<List<AppUser>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<AppUser?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}