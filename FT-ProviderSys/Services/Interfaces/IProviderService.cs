using FT_ProviderSys.DTOs;
using FT_ProviderSys.Models;

namespace FT_ProviderSys.Services.Interfaces
{
    public interface IProviderService
    {
        List<Provider> GetAll();
        Provider? GetById(int id);
        Task<int> Create(ProviderCreateRequestDTO inputProvider);
        bool Update(ProviderUpdateRequestDTO updatedProvider);
        bool Delete(int id);
    }
}
