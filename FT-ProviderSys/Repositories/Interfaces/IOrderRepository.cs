using FT_ProviderSys.Models;

namespace FT_ProviderSys.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task Add(Order order);
    }
}
