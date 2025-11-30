using Microsoft.EntityFrameworkCore;
using CakeShopAPIAmazonQ.Data;
using CakeShopAPIAmazonQ.Models;

namespace CakeShopAPIAmazonQ.Services;

public class CartService : ICartService
{
    private readonly CakeShopContext _context;

    public CartService(CakeShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cart>> GetAllAsync()
    {
        return await _context.Carts.Include(c => c.User).Include(c => c.CartItems).ThenInclude(ci => ci.Cake).ToListAsync();
    }

    public async Task<Cart?> GetByIdAsync(int id)
    {
        return await _context.Carts.Include(c => c.User).Include(c => c.CartItems).ThenInclude(ci => ci.Cake).FirstOrDefaultAsync(c => c.CartId == id);
    }

    public async Task<Cart> CreateAsync(Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart?> UpdateAsync(int id, Cart cart)
    {
        var existing = await _context.Carts.FindAsync(id);
        if (existing == null) return null;

        existing.UserId = cart.UserId;
        
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cart = await _context.Carts.FindAsync(id);
        if (cart == null) return false;

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();
        return true;
    }
}