namespace CryptoPriceTracker.Api.Entity.Models
{
    public class CryptoAsset
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public string? ExternalId { get; set; }
        public Uri? IconUrl { get; set; }
        public ICollection<CryptoPriceHistory>? PriceHistory { get; set; }
    }
}