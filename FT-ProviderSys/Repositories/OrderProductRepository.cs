using FT_ProviderSys.Data;
using FT_ProviderSys.Models;
using FT_ProviderSys.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FT_ProviderSys.Repositories
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly Context _context;

        public OrderProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderProduct>?> GetByOrderId(int orderId)
        {
            try
            {
                return await _context.OrderProduct
                    .Where(x => x.OrderId == orderId)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderProduct?> GetByIds(int orderId, int productId)
        {
            try
            {
                return await _context.OrderProduct
                    .FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Add(OrderProduct orderProduct)
        {
            try
            {
                await _context.OrderProduct.AddAsync(orderProduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddRange(IEnumerable<OrderProduct> orderProducts)
        {
            try
            {
                await _context.OrderProduct.AddRangeAsync(orderProducts);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task DeleteByOrderId(int orderId)
        {
            try
            {
                var orderProducts = await GetByOrderId(orderId);
                _context.OrderProduct.RemoveRange(orderProducts);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> ExistsByOrderIdAsync(int orderId)
        {
            try
            {
                return await _context.OrderProduct
                    .AnyAsync(x => x.OrderId == orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ExistsByProductIdAsync(int productId)
        {
            try
            {
                return await _context.OrderProduct
                    .AnyAsync(x => x.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
