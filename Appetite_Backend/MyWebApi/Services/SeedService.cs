using MyWebApi.Data;
using MyWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MyWebApi.Services;

public class SeedService
{
    private readonly AppDbContext _context;
    private readonly IPasswordService _passwordService;

    public SeedService(AppDbContext context, IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public async Task SeedInitialDataAsync()
    {
        // Check if users already exist
        if (await _context.Users.AnyAsync())
            return;

        var users = new[]
        {
            new DbUser
            {
                Id = "usr-001",
                Name = "System Admin",
                Email = "admin@appetitechecker.com",
                PasswordHash = _passwordService.HashPassword("Admin123!"),
                Roles = "admin",
                OrganizationId = "org-001",
                OrganizationName = "System Organization",
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                AuthProvider = "local",
                FailedLoginAttempts = 0
            },
            new DbUser
            {
                Id = "usr-002",
                Name = "John Carrier",
                Email = "carrier@example.com",
                PasswordHash = _passwordService.HashPassword("Admin123!"),
                Roles = "carrier",
                OrganizationId = "org-002",
                OrganizationName = "ABC Insurance",
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                AuthProvider = "local",
                FailedLoginAttempts = 0
            },
            new DbUser
            {
                Id = "usr-003",
                Name = "Jane Agent",
                Email = "agent@example.com",
                PasswordHash = _passwordService.HashPassword("Admin123!"),
                Roles = "agent",
                OrganizationId = "org-003",
                OrganizationName = "XYZ Brokerage",
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                AuthProvider = "local",
                FailedLoginAttempts = 0
            }
        };

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();
    }
}