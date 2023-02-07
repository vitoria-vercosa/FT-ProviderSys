using FT_ProviderSys.Models;

namespace FT_ProviderSys.Repositories.Interfaces
{
    public interface IOrderProductRepository
    {
        Task<IEnumerable<OrderProduct>?> GetByOrderId(int orderId);
        Task<OrderProduct?> GetByIds(int orderId, int productId);
        Task Add(OrderProduct orderProduct);
        Task AddRange(IEnumerable<OrderProduct> orderProducts);
        //Task Delete(int id);
        Task DeleteByOrderId(int orderId);
        Task<bool> ExistsByOrderIdAsync(int orderId);
        Task<bool> ExistsByProductIdAsync(int productId);
    }
}
