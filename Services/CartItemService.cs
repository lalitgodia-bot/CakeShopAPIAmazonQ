using Microsoft.EntityFrameworkCore;
using CakeShopAPIAmazonQ.Data;
using CakeShopAPIAmazonQ.Models;

namespace CakeShopAPIAmazonQ.Services;

public class CartItemService : ICartItemService
{
    private readonly CakeShopContext _context;

    public CartItemService(CakeShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CartItem>> GetAllAsync()
    {
        return await _context.CartItems.Include(ci => ci.Cart).Include(ci => ci.Cake).ToListAsync();
    }

    public async Task<CartItem?> GetByIdAsync(int id)
    {
        return await _context.CartItems.Include(ci => ci.Cart).Include(ci => ci.Cake).FirstOrDefaultAsync(ci => ci.CartItemId == id);
    }

    public async Task<CartItem> CreateAsync(CartItem cartItem)
    {
        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();
        return cartItem;
    }

    public async Task<CartItem?> UpdateAsync(int id, CartItem cartItem)
    {
        var existing = await _context.CartItems.FindAsync(id);
        if (existing == null) return null;

        existing.CartId = cartItem.CartId;
        existing.CakeId = cartItem.CakeId;
        existing.Quantity = cartItem.Quantity;
        
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem == null) return false;

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
        return true;
    }
}