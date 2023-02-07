using FT_ProviderSys.Data;
using FT_ProviderSys.Models;
using FT_ProviderSys.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FT_ProviderSys.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly Context _context;

        public ProviderRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Provider>> GetAll()
        {
            try
            {
                return await _context.Provider.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Provider?> GetById(int id)
        {
            try
            {
                return await _context.Provider.FirstOrDefaultAsync(x => x.ProviderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Add(Provider provider)
        {
            try
            {
                await _context.Provider.AddAsync(provider);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task Update(Provider provider)
        {
            try
            {
                _context.Provider.Update(provider);
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
                var provider = await GetById(id);
                _context.Provider.Remove(provider);
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
                return await _context.Provider.AnyAsync(x => x.ProviderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
