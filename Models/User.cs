namespace CakeShopAPIAmazonQ.Models;

public enum UserRole
{
    Admin,
    Customer
}

public class User
{
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Customer;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Address { get; set; } = string.Empty;
    public DateTime PasswordLastUpdated { get; set; } = DateTime.UtcNow;
}