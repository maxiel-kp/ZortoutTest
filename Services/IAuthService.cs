
using ZortouTest.DTOs.Authen;

namespace ZortouTest.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);
    }
}