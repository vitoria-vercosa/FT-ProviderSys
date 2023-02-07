using FT_ProviderSys.DTOs;
using FT_ProviderSys.Models;

namespace FT_ProviderSys.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product?> GetById(int id);
        Task<double> GetAmountByProductIds(IEnumerable<int> productIds);
        Task<int> Create(ProductCreateRequestDTO inputProduct);
        Task<bool> Update(ProductUpdateRequestDTO inputProduct);
        Task<bool> Delete(int id);
    }
}
