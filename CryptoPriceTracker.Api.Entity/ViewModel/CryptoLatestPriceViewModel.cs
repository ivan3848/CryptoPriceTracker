namespace CryptoPriceTracker.Api.Entity.ViewModel
{
    public class CryptoLatestPriceViewModel
    {
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public string? ExternalId { get; set; }
        public Uri? IconUrl { get; set; }
        public decimal? Price { get; set; }
        public decimal? PreviousPrice { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
