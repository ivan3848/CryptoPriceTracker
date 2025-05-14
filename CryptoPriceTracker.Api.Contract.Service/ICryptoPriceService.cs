using CryptoPriceTracker.Api.Entity.ViewModel;

namespace CryptoPriceTracker.Api.Contract.Service
{
    public interface ICryptoPriceService
    {
        Task UpdatePricesAsync();
        Task<List<CryptoLatestPriceViewModel>> GetLatestPrices(int page = 1, int pageSize = 10);
    }
}
