using FT_ProviderSys.DTOs;
using FT_ProviderSys.Models;

namespace FT_ProviderSys.Services.Interfaces
{
    public interface IProviderService
    {
        Task<IEnumerable<Provider>> GetAll();
        Task<Provider?> GetById(int id);
        Task<int> Create(ProviderCreateRequestDTO inputProvider);
        Task<bool> Update(ProviderUpdateRequestDTO inputProvider);
        Task<bool> Delete(int id);
    }
}
