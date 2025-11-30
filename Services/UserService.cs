using Microsoft.EntityFrameworkCore;
using CakeShopAPIAmazonQ.Data;
using CakeShopAPIAmazonQ.Models;

namespace CakeShopAPIAmazonQ.Services;

public class UserService : IUserService
{
    private readonly CakeShopContext _context;

    public UserService(CakeShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(int id, User user)
    {
        var existing = await _context.Users.FindAsync(id);
        if (existing == null) return null;

        existing.FullName = user.FullName;
        existing.Email = user.Email;
        existing.MobileNo = user.MobileNo;
        existing.Role = user.Role;
        existing.Address = user.Address;
        
        if (!string.IsNullOrEmpty(user.PasswordHash))
        {
            existing.PasswordHash = user.PasswordHash;
            existing.PasswordLastUpdated = DateTime.UtcNow;
        }
        
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}