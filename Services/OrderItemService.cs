using Microsoft.EntityFrameworkCore;
using CakeShopAPIAmazonQ.Data;
using CakeShopAPIAmazonQ.Models;

namespace CakeShopAPIAmazonQ.Services;

public class OrderItemService : IOrderItemService
{
    private readonly CakeShopContext _context;

    public OrderItemService(CakeShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync()
    {
        return await _context.OrderItems.Include(oi => oi.Order).Include(oi => oi.Cake).ToListAsync();
    }

    public async Task<OrderItem?> GetByIdAsync(int id)
    {
        return await _context.OrderItems.Include(oi => oi.Order).Include(oi => oi.Cake).FirstOrDefaultAsync(oi => oi.OrderItemId == id);
    }

    public async Task<OrderItem> CreateAsync(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
        return orderItem;
    }

    public async Task<OrderItem?> UpdateAsync(int id, OrderItem orderItem)
    {
        var existing = await _context.OrderItems.FindAsync(id);
        if (existing == null) return null;

        existing.OrderId = orderItem.OrderId;
        existing.CakeId = orderItem.CakeId;
        existing.Quantity = orderItem.Quantity;
        existing.UnitPrice = orderItem.UnitPrice;
        
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        if (orderItem == null) return false;

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
        return true;
    }
}