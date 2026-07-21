namespace ZortouTest.DTOs.Sales
{
    public class SalesSummaryRequest
    {
        public DateTime StartDate { get; init; }

        public DateTime EndDate { get; init; }

        public List<int> ProductCodes { get; init; } = [];
    }
}
