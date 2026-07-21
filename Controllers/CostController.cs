using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using ZortouTest.DALs.ExternalServices;
using ZortouTest.DTOs.Common;
using ZortouTest.DTOs.Cost;
using ZortouTest.Services.Costs;

namespace ZortouTest.Controllers
{
    [ApiController]
    [Route("api/cost")]
    public class CostController : ControllerBase
    {
        private readonly ICostService _costService;

        public CostController(ICostService costService)
        {
            _costService = costService;
        }

        [HttpGet]
        public async Task<ActionResult<
        ApiResponse<IReadOnlyList<CostResponse>>>> GetCosts(
        [FromQuery] CostRequest request,
        CancellationToken cancellationToken)
        {
            var authorizationHeader =
                Request.Headers.Authorization.ToString();

            if (!AuthenticationHeaderValue.TryParse(
                    authorizationHeader,
                    out var authenticationHeader) ||
                !string.Equals(
                    authenticationHeader.Scheme,
                    "Bearer",
                    StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(authenticationHeader.Parameter))
            {
                throw new AppException(
                    code: "BEARER_TOKEN_REQUIRED",
                    message: "Authorization header with Bearer token is required.",
                    statusCode: StatusCodes.Status401Unauthorized);
            }

            var result = await _costService.GetCostsAsync(
                authenticationHeader.Parameter,
                request,
                cancellationToken);

            return Ok(
                ApiResponse<IReadOnlyList<CostResponse>>
                    .Ok(result));
        }
    }
}
