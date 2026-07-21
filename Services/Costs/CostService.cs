using ZortouTest.DALs.ExternalServices;
using ZortouTest.DTOs.Cost;

namespace ZortouTest.Services.Costs
{
    public class CostService : ICostService
    {
        private IZortApiClient _zortApiClient;

        public CostService(IZortApiClient zortApiClient)
        {
            _zortApiClient = zortApiClient;
        }

        public async Task<IReadOnlyList<CostResponse>> GetCostsAsync(
        string bearerToken,
        CostRequest request,
        CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(bearerToken))
            {
                throw new AppException(
                    code: "BEARER_TOKEN_REQUIRED",
                    message: "Bearer token is required.",
                    statusCode: StatusCodes.Status401Unauthorized);
            }

            if (request.EndDate < request.StartDate)
            {
                throw new AppException(
                    code: "INVALID_DATE_RANGE",
                    message: "End date must be greater than or equal to start date.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            var productCodes = request.ProductCodes
                .Where(code => code > 0)
                .Distinct()
                .ToArray();

            var costs = await _zortApiClient.GetCostsAsync(
                bearerToken,
                request.StartDate,
                request.EndDate,
                productCodes,
                cancellationToken);

            return costs
                .Select(cost => new CostResponse
                {
                    ProductCode = cost.ProductCode,
                    Cost = cost.Cost
                })
                .ToArray();
        }
    }
}