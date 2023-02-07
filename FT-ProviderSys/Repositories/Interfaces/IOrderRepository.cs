using FT_ProviderSys.Models;

namespace FT_ProviderSys.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order?> GetById(int id);
        Task<IEnumerable<Order>> GetByProviderId(int providerId);
        Task Add(Order order);
        Task Update(Order order);
        Task Delete(int id);
        Task<bool> ExistAsync(int id);
        Task<bool> ExistByProvider(int providerId);
    }
}
