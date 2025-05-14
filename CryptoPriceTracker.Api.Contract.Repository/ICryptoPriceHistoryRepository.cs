using CryptoPriceTracker.Api.Entity.Models;

namespace CryptoPriceTracker.Api.Contract.Repository
{
    public interface ICryptoPriceHistoryRepository
    {
        Task<IEnumerable<CryptoPriceHistory>> GetAll();
        Task AddInRange(List<CryptoPriceHistory> histories);
    }
}
