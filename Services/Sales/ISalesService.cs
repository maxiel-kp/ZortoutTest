using ZortouTest.DTOs.Sales;

namespace ZortouTest.Services.Sales
{
    public interface ISalesService
    {
        Task<IReadOnlyList<SalesSummaryResponse>> GetSummaryAsync(
        SalesSummaryRequest request,
        CancellationToken cancellationToken = default);
    }
}