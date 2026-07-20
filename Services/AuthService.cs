using ZortouTest.DALs.ExternalServices;
using ZortouTest.DTOs.Authen;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZortouTest.Services
{
    public class AuthService : IAuthService
    {
        private readonly IZortApiClient _zortApiClient;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IZortApiClient zortApiClient,
            ILogger<AuthService> logger)
        {
            _zortApiClient = zortApiClient;
            _logger = logger;
        }

        public async Task<LoginResponse> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default)
        {
            ValidateRequest(request);

            var zortResponse = await _zortApiClient.LoginAsync(
                request.Username.Trim(),
                request.Password,
                cancellationToken);

            if (string.IsNullOrWhiteSpace(zortResponse.Data.AccessToken))
            {
                throw new AppException(
                    code: "INVALID_AUTH_RESPONSE",
                    message: "Authentication service returned an invalid token.",
                    statusCode: StatusCodes.Status502BadGateway);
            }

            _logger.LogInformation(
                "User authentication completed for {Username}",
                request.Username);

            return new LoginResponse
            {
                Token = zortResponse.Data.AccessToken,
            };
        }

        private static void ValidateRequest(LoginRequest request)
        {
            if (request is null)
            {
                throw new AppException(
                    code: "REQUEST_REQUIRED",
                    message: "Login request is required.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new AppException(
                    code: "USERNAME_REQUIRED",
                    message: "Username is required.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new AppException(
                    code: "PASSWORD_REQUIRED",
                    message: "Password is required.",
                    statusCode: StatusCodes.Status400BadRequest);
            }
        }
    }
}
