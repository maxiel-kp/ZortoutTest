using System.Text.Json.Serialization;

namespace ZortouTest.DALs.ExternalServices
{
    public class ZortCostApiItem
    {
        [JsonPropertyName("productCode")]
        public int ProductCode { get; init; }

        [JsonPropertyName("cost")]
        public decimal Cost { get; init; }
    }
}