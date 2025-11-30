namespace CakeShopAPIAmazonQ.Models;

public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    
    public User? User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}