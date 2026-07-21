using ZortouTest.Models.Temps;

namespace ZortouTest.DALs.Sales
{
    public interface ISalesRepository
    {
        Task<IReadOnlyList<SalesSummaryModel>> GetSummaryAsync(
        DateTime startDate,
        DateTime endExclusive,
        IReadOnlyCollection<int> productCodes,
        CancellationToken cancellationToken = default);
    }
}
