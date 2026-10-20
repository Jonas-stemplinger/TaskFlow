using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskFlow.Data;
using TaskFlow.DTOs;
using TaskFlow.Models;

namespace TaskFlow.Services
{
    public class AuthService
    {
        private readonly TaskFlowDbContext _context;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AuthService(TaskFlowDbContext context, IPasswordHasher<AppUser> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AppUser?> LoginAsync(LoginRequest request)
        {
            string email = request.Email.Trim().ToLowerInvariant();

            // Email vom User suchen
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) { return null; }

            //Hier wird falls noch klartext in der Datenbank steht, das Überprüft. Jeder Hash beginnt mit AQAAAA
            //Beginnt quasi nur bei dem Identity library so --> Base64 Kodierung
            bool isHashed = user.Password.StartsWith("AQAAAA");

            if (!isHashed)
            {
                if (user.Password != request.Password) { return null; }

                user.Password = _passwordHasher.HashPassword(user, request.Password);
                await _context.SaveChangesAsync();

                return user;
            }

            
            
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (result == PasswordVerificationResult.Failed) { return null; }

            return user;
        }
    }
}
