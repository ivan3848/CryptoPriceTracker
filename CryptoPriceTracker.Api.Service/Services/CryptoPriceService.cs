

using CryptoPriceTracker.Api.Contract.Repository;
using CryptoPriceTracker.Api.Contract.Service;
using CryptoPriceTracker.Api.Entity.Models;
using CryptoPriceTracker.Api.Entity.ViewModel;
using CryptoPriceTracker.Api.Service.Validators;
using Newtonsoft.Json;

namespace CryptoPriceTracker.Api.Service.Services
{
    public class CryptoPriceService : ICryptoPriceService
    {
        private readonly HttpClient _httpClient;
        private readonly ICryptoAssetRepository _cryptoAssetRepository;
        private readonly ICryptoPriceHistoryRepository _cryptoPriceHistoryRepository;

        public CryptoPriceService(ICryptoAssetRepository cryptoAssetRepository, ICryptoPriceHistoryRepository cryptoPriceHistoryRepository,
            HttpClient httpClient)
        {
            _httpClient = httpClient;
            _cryptoAssetRepository = cryptoAssetRepository;
            _cryptoPriceHistoryRepository = cryptoPriceHistoryRepository;
        }

        public async Task UpdatePricesAsync()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd"
            );
            request.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; AcmeInc/1.0)");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            var json = await response.Content.ReadAsStringAsync();
            var currencies = JsonConvert.DeserializeObject<List<CurrencyViewModel>>(json);

            var cryptoAssets = await _cryptoAssetRepository.GetAll();
            var assetsByExternalId = cryptoAssets.ToDictionary(a => a.ExternalId, StringComparer.OrdinalIgnoreCase);

            var existingHistory = (await _cryptoPriceHistoryRepository.GetAll())
                .Select(h => (h.CryptoAsset.ExternalId, h.Date, h.Price))
                .ToHashSet();


            var validator = new PriceValidator(existingHistory);
            var newAssets = new List<CryptoAsset>();
            var newPriceHistories = new List<CryptoPriceHistory>();

            foreach (var currency in currencies)
            {
                if (!assetsByExternalId.TryGetValue(currency.Id, out var asset))
                {
                    asset = new CryptoAsset
                    {
                        ExternalId = currency.Id,
                        Name = currency.Name,
                        Symbol = currency.Symbol,
                        IconUrl = currency.Image
                    };
                    newAssets.Add(asset);

                    // This is to ensure that the crypto is not repeated in case the JSON from the CoinGecko API contains duplicates
                    assetsByExternalId[currency.Id] = asset;
                }

                var priceEntry = (currency.Id, currency.LastUpdated, currency.CurrentPrice);
                if (validator.ShouldSavePrice(priceEntry))
                {
                    newPriceHistories.Add(new CryptoPriceHistory
                    {
                        CryptoAsset = asset,
                        Price = currency.CurrentPrice,
                        Date = currency.LastUpdated
                    });
                }
            }

            if (newAssets.Any())
            {
                await _cryptoAssetRepository.AddInRange(newAssets);
            }

            if (newPriceHistories.Any())
            {
                foreach (var history in newPriceHistories)
                {
                    history.CryptoAssetId = history.CryptoAsset.Id;
                    history.CryptoAsset = null;
                }

                await _cryptoPriceHistoryRepository.AddInRange(newPriceHistories);
            }
        }

        public async Task<List<CryptoLatestPriceViewModel>> GetLatestPrices(int page = 1, int pageSize = 10)
        {
            var skip = (page - 1) * pageSize;

            var cryptoAssets = await _cryptoAssetRepository
                .GetForLatestPrice(skip, pageSize)
                .ConfigureAwait(false);

            return cryptoAssets.Select(asset => new CryptoLatestPriceViewModel
            {
                Name = asset.Name,
                Symbol = asset.Symbol,
                ExternalId = asset.ExternalId,
                IconUrl = asset.IconUrl,
                Price = asset.PriceHistory.FirstOrDefault()?.Price,
                PreviousPrice = asset.PriceHistory.Skip(1).FirstOrDefault()?.Price,
                LastUpdated = asset.PriceHistory.FirstOrDefault()?.Date
            }).ToList();
        }

    }
}
