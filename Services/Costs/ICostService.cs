using ZortouTest.DTOs.Cost;

namespace ZortouTest.Services.Costs
{
    public interface ICostService
    {
        Task<IReadOnlyList<CostResponse>> GetCostsAsync(
        string bearerToken,
        CostRequest request,
        CancellationToken cancellationToken = default);
    }
}