using ZortouTest.DTOs.Authen;

namespace ZortouTest.Services.Authen
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);
    }
}