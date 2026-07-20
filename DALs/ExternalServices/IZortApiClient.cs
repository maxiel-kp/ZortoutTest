using ZortouTest.DTOs.Authen;

namespace ZortouTest.DALs.ExternalServices
{
    public interface IZortApiClient
    {
        Task<ZortLoginResult> LoginAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default);
    }
}
