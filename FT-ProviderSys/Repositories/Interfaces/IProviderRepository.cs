using FT_ProviderSys.Models;

namespace FT_ProviderSys.Repositories.Interfaces
{
    public interface IProviderRepository
    {
        Task Add(Provider provider);
    }
}
