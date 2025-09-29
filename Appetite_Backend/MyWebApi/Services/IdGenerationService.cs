using MyWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MyWebApi.Services;

public interface IIdGenerationService
{
    Task<string> GenerateUserIdAsync();
    Task<string> GenerateCarrierIdAsync();
    Task<string> GenerateOrganizationIdAsync();
    Task<string> GenerateProductIdAsync();
}

public class IdGenerationService : IIdGenerationService
{
    private readonly AppDbContext _context;

    public IdGenerationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateUserIdAsync()
    {
        var count = await _context.Users.CountAsync();
        return $"usr-{(count + 1):D3}";
    }

    public async Task<string> GenerateCarrierIdAsync()
    {
        var count = await _context.Carriers.CountAsync();
        return $"car-{(count + 1):D3}";
    }

    public async Task<string> GenerateOrganizationIdAsync()
    {
        var userCount = await _context.Users.CountAsync();
        return $"org-{(userCount + 1):D3}";
    }

    public async Task<string> GenerateProductIdAsync()
    {
        // Since products are in-memory, we need to count them differently
        // This is a temporary solution until products are moved to database
        var existingProductsCount = 3; // Current hardcoded products (prod-001, prod-002, prod-003)
        
        // In a real implementation, this would count from Products table
        // var count = await _context.Products.CountAsync();
        
        return $"prod-{(existingProductsCount + 1):D3}";
    }
}