using CryptoPriceTracker.Api.Entity.Models;

namespace CryptoPriceTracker.Api.Contract.Repository
{
    public interface ICryptoAssetRepository
    {
        Task<IEnumerable<CryptoAsset>> GetAll();
        Task<IEnumerable<CryptoAsset>> GetForLatestPrice();
        Task AddInRange(List<CryptoAsset> assets);
        Task<IEnumerable<CryptoAsset>> GetForLatestPrice(int skip, int take);
    }
}
