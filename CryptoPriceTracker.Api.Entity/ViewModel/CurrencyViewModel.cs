using Newtonsoft.Json;

namespace CryptoPriceTracker.Api.Entity.ViewModel
{
    public partial class CurrencyViewModel
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("symbol")]
        public string? Symbol { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("image")]
        public Uri? Image { get; set; }

        [JsonProperty("current_price")]
        public decimal CurrentPrice { get; set; }

        [JsonProperty("market_cap")]
        public long MarketCap { get; set; }

        [JsonProperty("market_cap_rank")]
        public long MarketCapRank { get; set; }

        [JsonProperty("fully_diluted_valuation")]
        public long FullyDilutedValuation { get; set; }

        [JsonProperty("total_volume")]
        public long TotalVolume { get; set; }

        [JsonProperty("high_24h")]
        public double High24H { get; set; }

        [JsonProperty("low_24h")]
        public double Low24H { get; set; }

        [JsonProperty("price_change_24h")]
        public double PriceChange24H { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public double PriceChangePercentage24H { get; set; }

        [JsonProperty("market_cap_change_24h")]
        public long MarketCapChange24H { get; set; }

        [JsonProperty("market_cap_change_percentage_24h")]
        public double MarketCapChangePercentage24H { get; set; }

        [JsonProperty("circulating_supply")]
        public double CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public double TotalSupply { get; set; }

        [JsonProperty("max_supply")]
        public object? MaxSupply { get; set; }

        [JsonProperty("ath")]
        public double Ath { get; set; }

        [JsonProperty("ath_change_percentage")]
        public double AthChangePercentage { get; set; }

        [JsonProperty("ath_date")]
        public DateTime AthDate { get; set; }

        [JsonProperty("atl")]
        public double Atl { get; set; }

        [JsonProperty("atl_change_percentage")]
        public double AtlChangePercentage { get; set; }

        [JsonProperty("atl_date")]
        public DateTime AtlDate { get; set; }

        [JsonProperty("roi")]
        public Roi? Roi { get; set; }

        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
    }

    public partial class Roi
    {
        [JsonProperty("times")]
        public double Times { get; set; }

        [JsonProperty("currency")]
        public string? Currency { get; set; }

        [JsonProperty("percentage")]
        public double Percentage { get; set; }
    }

}
