using FT_ProviderSys.Data;
using FT_ProviderSys.Models;
using FT_ProviderSys.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FT_ProviderSys.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;

        public OrderRepository(Context context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Order>> GetAll()
        {
            try
            {
                return await _context.Order.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order?> GetById(int id)
        {
            try
            {
                return await _context.Order.FirstOrDefaultAsync(x => x.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Order>> GetByProviderId(int providerId)
        {
            try
            {
                return await _context.Order
                    .Where(x => x.ProviderId == providerId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Add(Order order)
        {
            try
            {
                await _context.Order.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task Update(Order order)
        {
            try
            {
                _context.Order.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task Delete(int id)
        {
            try
            {
                var order = await GetById(id);
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> ExistAsync(int id)
        {
            try
            {
                return await _context.Order.AnyAsync(x => x.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ExistByProvider(int providerId)
        {
            try
            {
                return await _context.Order.AnyAsync(x => x.ProviderId == providerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
