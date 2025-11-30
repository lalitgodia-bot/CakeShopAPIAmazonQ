namespace CakeShopAPIAmazonQ.Models;

public class CartItem
{
    public int CartItemId { get; set; }
    public int CartId { get; set; }
    public int CakeId { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    
    public Cart? Cart { get; set; }
    public Cake? Cake { get; set; }
}