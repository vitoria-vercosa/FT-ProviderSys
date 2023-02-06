using FT_ProviderSys.Data;
using FT_ProviderSys.Models;
using FT_ProviderSys.Repositories.Interfaces;

namespace FT_ProviderSys.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;

        public OrderRepository(Context context)
        {
            _context = context;
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
    }
}
