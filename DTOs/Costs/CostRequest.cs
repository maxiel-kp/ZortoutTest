namespace ZortouTest.DTOs.Cost
{
    public class CostRequest
    {
        public DateTime StartDate { get; internal set; }
        public DateTime EndDate { get; internal set; }
        public List<int> ProductCodes { get; init; } = [];
    }
}