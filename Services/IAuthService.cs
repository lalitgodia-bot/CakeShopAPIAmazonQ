using CakeShopAPIAmazonQ.Models;

namespace CakeShopAPIAmazonQ.Services;

public interface IAuthService
{
    Task<string?> LoginAsync(string email, string password);
    string GenerateJwtToken(User user);
}