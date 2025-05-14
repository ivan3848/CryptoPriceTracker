using CryptoPriceTracker.Api.Service.Validators;

namespace CryptoPriceTracker.Tests
{
    public class PriceHistoryValidatorTests
    {
        [Fact]
        public void ShouldNotAdd_WhenExactMatchExists()
        {
            var existing = new List<(string, DateTime, decimal)>
            {
                ("btc", new DateTime(2025, 05, 01, 12, 0, 0), 50000m)
            };

            var validator = new PriceValidator(existing);

            var current = ("btc", new DateTime(2025, 05, 01, 12, 0, 0), 50000m);
            var result = validator.ShouldSavePrice(current);

            Assert.False(result);
        }

        [Fact]
        public void ShouldAdd_WhenPriceIsDifferent()
        {
            var existing = new List<(string, DateTime, decimal)>
            {
                ("btc", new DateTime(2025, 05, 01, 12, 0, 0), 50000m)
            };

            var validator = new PriceValidator(existing);

            var current = ("btc", new DateTime(2025, 05, 01, 12, 0, 0), 51000m);
            var result = validator.ShouldSavePrice(current);

            Assert.True(result);
        }

        [Fact]
        public void ShouldAdd_WhenDateIsDifferent()
        {
            var existing = new List<(string, DateTime, decimal)>
            {
                ("btc", new DateTime(2025, 05, 01, 12, 0, 0), 50000m)
            };

            var validator = new PriceValidator(existing);

            var current = ("btc", new DateTime(2025, 05, 01, 13, 0, 0), 50000m);
            var result = validator.ShouldSavePrice(current);

            Assert.True(result);
        }
    }
}
