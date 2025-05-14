namespace CryptoPriceTracker.Api.Service.Validators
{
    public class PriceValidator
    {
        private readonly HashSet<(string ExternalId, DateTime Date, decimal Price)> _existing;

        public PriceValidator(IEnumerable<(string ExternalId, DateTime Date, decimal Price)> existing)
        {
            _existing = new HashSet<(string, DateTime, decimal)>(existing);
        }

        public bool ShouldSavePrice((string Id, DateTime Date, decimal Price) priceEntry)
        {
            // Removed .Date to retain time precision—crypto values can change within the same day, even in minutes or hours.
            return !_existing.Contains(priceEntry);
        }
    }
}