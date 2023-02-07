using FT_ProviderSys.DTOs;
using FT_ProviderSys.Models;

namespace FT_ProviderSys.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order?> GetById(int id);
        Task<IEnumerable<Order>> GetByProviderId(int providerId);
        Task<int> Create(OrderCreateRequestDTO inputOrder);
        Task<bool> Update(OrderUpdateRequestDTO inputOrder);
        Task<bool> Delete(int id);
    }
}