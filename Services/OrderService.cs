using Microsoft.EntityFrameworkCore;
using CakeShopAPIAmazonQ.Data;
using CakeShopAPIAmazonQ.Models;

namespace CakeShopAPIAmazonQ.Services;

public class OrderService : IOrderService
{
    private readonly CakeShopContext _context;

    public OrderService(CakeShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders.Include(o => o.User).Include(o => o.OrderItems).ThenInclude(oi => oi.Cake).ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders.Include(o => o.User).Include(o => o.OrderItems).ThenInclude(oi => oi.Cake).FirstOrDefaultAsync(o => o.OrderId == id);
    }

    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> UpdateAsync(int id, Order order)
    {
        var existing = await _context.Orders.FindAsync(id);
        if (existing == null) return null;

        existing.UserId = order.UserId;
        existing.TotalAmount = order.TotalAmount;
        existing.ShippingAddress = order.ShippingAddress;
        existing.Status = order.Status;
        existing.PaymentStatus = order.PaymentStatus;
        
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return false;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }
}