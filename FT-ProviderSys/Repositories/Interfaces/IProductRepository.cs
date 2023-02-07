using FT_ProviderSys.Models;

namespace FT_ProviderSys.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product?> GetById(int id);
        Task<double> GetAmountByProductIds(IEnumerable<int> productIds);
        Task<double> GetPrice(int productId);
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(int id);
        Task<bool> ExistAsync(int id);
        Task<bool> ExistAsync(IEnumerable<int> productIds);
    }
}
