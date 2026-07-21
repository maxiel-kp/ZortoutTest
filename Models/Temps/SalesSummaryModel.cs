namespace ZortouTest.Models.Temps
{
    public class SalesSummaryModel
    {
        public int ProductCode { get; init; }

        public string? ProductName { get; init; }

        public int TotalQuantity { get; init; }

        public decimal TotalSales { get; init; }
    }
}
