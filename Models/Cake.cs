namespace CakeShopAPIAmazonQ.Models;

public class Cake
{
    public int CakeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public Category? Category { get; set; }
}