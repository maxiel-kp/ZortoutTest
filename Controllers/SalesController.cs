using Microsoft.AspNetCore.Mvc;
using ZortouTest.DTOs.Common;
using ZortouTest.DTOs.Sales;
using ZortouTest.Services.Sales;

namespace ZortouTest.Controllers
{

    [ApiController]
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<
       ApiResponse<IReadOnlyList<SalesSummaryResponse>>>> GetSummary(
       [FromQuery] SalesSummaryRequest request,
       CancellationToken cancellationToken)
        {
            var result = await _salesService.GetSummaryAsync(
                request,
                cancellationToken);

            return Ok(ApiResponse<IReadOnlyList<SalesSummaryResponse>>
                    .Ok(result));
        }
    }
}
