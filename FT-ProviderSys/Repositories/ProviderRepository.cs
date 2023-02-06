using FT_ProviderSys.Data;
using FT_ProviderSys.Models;
using FT_ProviderSys.Repositories.Interfaces;

namespace FT_ProviderSys.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly Context _context;

        public ProviderRepository(Context context)
        {
            _context = context;
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
    }
}
