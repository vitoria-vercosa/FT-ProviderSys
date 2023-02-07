using FT_ProviderSys.Models;

namespace FT_ProviderSys.Repositories.Interfaces
{
    public interface IProviderRepository
    {
        Task<IEnumerable<Provider>> GetAll();
        Task<Provider?> GetById(int id);
        Task Add(Provider provider);
        Task Update(Provider provider);
        Task Delete(int id);
        Task<bool> ExistAsync(int id);

    }
}
