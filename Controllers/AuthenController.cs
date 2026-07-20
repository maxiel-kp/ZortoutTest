using Microsoft.AspNetCore.Mvc;
using ZortouTest.DTOs.Authen;
using ZortouTest.DTOs.Common;
using ZortouTest.Services;

namespace ZortouTest.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthenController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> Login(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(
            request,
            cancellationToken);

            return Ok(ApiResponse<LoginResponse>.Ok(result));
        }
    }
}
