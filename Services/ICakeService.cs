using CakeShopAPIAmazonQ.Models;

namespace CakeShopAPIAmazonQ.Services;

public interface ICakeService
{
    Task<IEnumerable<Cake>> GetAllAsync();
    Task<Cake?> GetByIdAsync(int id);
    Task<Cake> CreateAsync(Cake cake);
    Task<Cake?> UpdateAsync(int id, Cake cake);
    Task<bool> DeleteAsync(int id);
}