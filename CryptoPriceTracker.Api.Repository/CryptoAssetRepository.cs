using CryptoPriceTracker.Api.Contract.Repository;
using CryptoPriceTracker.Api.Entity.Data;
using CryptoPriceTracker.Api.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoPriceTracker.Api.Repository
{
    public class CryptoAssetRepository : ICryptoAssetRepository
    {
        private readonly ApplicationDbContext _context;

        public CryptoAssetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CryptoAsset>> GetAll() =>
            await _context.CryptoAssets.Include(h => h.PriceHistory).ToListAsync().ConfigureAwait(false);

        public async Task<IEnumerable<CryptoAsset>> GetForLatestPrice(int skip, int take)
        {
            return await _context.CryptoAssets
                .Include(asset => asset.PriceHistory)
                .OrderBy(asset => asset.Name)
                .Skip(skip)
                .Take(take)
                .Select(asset => new CryptoAsset
                {
                    Name = asset.Name,
                    Symbol = asset.Symbol,
                    ExternalId = asset.ExternalId,
                    IconUrl = asset.IconUrl,
                    PriceHistory = asset.PriceHistory
                        .OrderByDescending(h => h.Date)
                        .Take(2)
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task AddInRange(List<CryptoAsset> assets)
        {
            await _context.AddRangeAsync(assets).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task<IEnumerable<CryptoAsset>> GetForLatestPrice()
        {
            throw new NotImplementedException();
        }
    }
}
