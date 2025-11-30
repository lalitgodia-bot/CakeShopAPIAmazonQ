using Microsoft.EntityFrameworkCore;
using CakeShopAPIAmazonQ.Data;
using CakeShopAPIAmazonQ.Models;

namespace CakeShopAPIAmazonQ.Services;

public class CakeService : ICakeService
{
    private readonly CakeShopContext _context;

    public CakeService(CakeShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cake>> GetAllAsync()
    {
        return await _context.Cakes.Include(c => c.Category).ToListAsync();
    }

    public async Task<Cake?> GetByIdAsync(int id)
    {
        return await _context.Cakes.Include(c => c.Category).FirstOrDefaultAsync(c => c.CakeId == id);
    }

    public async Task<Cake> CreateAsync(Cake cake)
    {
        _context.Cakes.Add(cake);
        await _context.SaveChangesAsync();
        return cake;
    }

    public async Task<Cake?> UpdateAsync(int id, Cake cake)
    {
        var existing = await _context.Cakes.FindAsync(id);
        if (existing == null) return null;

        existing.Name = cake.Name;
        existing.CategoryId = cake.CategoryId;
        existing.Description = cake.Description;
        existing.Price = cake.Price;
        existing.ImageUrl = cake.ImageUrl;
        existing.IsAvailable = cake.IsAvailable;
        existing.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cake = await _context.Cakes.FindAsync(id);
        if (cake == null) return false;

        _context.Cakes.Remove(cake);
        await _context.SaveChangesAsync();
        return true;
    }
}