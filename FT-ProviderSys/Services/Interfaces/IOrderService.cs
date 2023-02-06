using FT_ProviderSys.DTOs;
using FT_ProviderSys.Models;

namespace FT_ProviderSys.Services.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order? GetById(int id);
        Task<int> Create(OrderCreateRequestDTO inputOrder);
        bool Update(OrderUpdateRequestDTO inputOrder);
        bool Delete(int id);
    }
}