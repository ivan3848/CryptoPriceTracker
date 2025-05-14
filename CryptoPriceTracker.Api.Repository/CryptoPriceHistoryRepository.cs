using CryptoPriceTracker.Api.Contract.Repository;
using CryptoPriceTracker.Api.Entity.Data;
using CryptoPriceTracker.Api.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoPriceTracker.Api.Repository
{
    public class CryptoPriceHistoryRepository : ICryptoPriceHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CryptoPriceHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CryptoPriceHistory>> GetAll() =>
            await _context.CryptoPriceHistories.Include(c => c.CryptoAsset).ToListAsync();

        public async Task AddInRange(List<CryptoPriceHistory> histories)
        {
            await _context.AddRangeAsync(histories).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
