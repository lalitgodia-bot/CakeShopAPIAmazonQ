using CakeShopAPIAmazonQ.Models;

namespace CakeShopAPIAmazonQ.Services;

public interface ICartItemService
{
    Task<IEnumerable<CartItem>> GetAllAsync();
    Task<CartItem?> GetByIdAsync(int id);
    Task<CartItem> CreateAsync(CartItem cartItem);
    Task<CartItem?> UpdateAsync(int id, CartItem cartItem);
    Task<bool> DeleteAsync(int id);
}