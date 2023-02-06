using FT_ProviderSys.DTOs;
using FT_ProviderSys.Models;

namespace FT_ProviderSys.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product? GetById(int id);
        int Create(ProductCreateRequestDTO product);
        bool Update(ProductUpdateRequestDTO updatedProduct);
        bool Delete(int id);
    }
}
