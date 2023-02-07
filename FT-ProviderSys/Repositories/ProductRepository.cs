using FT_ProviderSys.Data;
using FT_ProviderSys.Models;
using FT_ProviderSys.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FT_ProviderSys.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                return await _context.Product.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product?> GetById(int id)
        {
            try
            {
                return await _context.Product.FirstOrDefaultAsync(x => x.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<double> GetPrice(int productId)
        {
            try
            {
                var product = await _context.Product.FirstOrDefaultAsync(x => x.ProductId == productId);
                return product.Price;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<double> GetAmountByProductIds(IEnumerable<int> productIds)
        {
            try
            {
                var amount = await _context.Product
                    .Where(x => productIds.Contains(x.ProductId))
                    .SumAsync(x => x.Price);

                return amount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Add(Product product)
        {
            try
            {
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task Update(Product product)
        {
            try
            {
                _context.Product.Update(product);
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
                var product = await GetById(id);
                _context.Product.Remove(product);
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
                return await _context.Product.AnyAsync(x => x.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ExistAsync(IEnumerable<int> productIds)
        {
            try
            {
                return await _context.Product.AnyAsync(x => productIds.Contains(x.ProductId) );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
