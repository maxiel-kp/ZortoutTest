namespace ZortouTest.DTOs.Sales
{
    public class SalesSummaryResponse
    {
        public int ProductCode { get; init; }

        public string? ProductName { get; init; }

        public int TotalQuantity { get; init; }

        public decimal TotalSales { get; init; }
    }
}
