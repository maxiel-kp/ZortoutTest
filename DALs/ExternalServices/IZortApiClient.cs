using ZortouTest.DTOs.Authen;

namespace ZortouTest.DALs.ExternalServices
{
    public interface IZortApiClient
    {
        Task<IEnumerable<ZortCostResult>> GetCostsAsync(string bearerToken, DateTime startDate, DateTime endDate, int[] productCodes, CancellationToken cancellationToken);
        Task<ZortLoginResult> LoginAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default);
    }
}
