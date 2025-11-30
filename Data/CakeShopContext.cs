using Microsoft.EntityFrameworkCore;
using CakeShopAPIAmazonQ.Models;

namespace CakeShopAPIAmazonQ.Data;

public class CakeShopContext : DbContext
{
    public CakeShopContext(DbContextOptions<CakeShopContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Cake> Cakes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}