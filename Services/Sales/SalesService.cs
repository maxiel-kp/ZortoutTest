using ZortouTest.DALs.ExternalServices;
using ZortouTest.DALs.Sales;
using ZortouTest.DTOs.Sales;

namespace ZortouTest.Services.Sales
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;

        public SalesService(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<IReadOnlyList<SalesSummaryResponse>> GetSummaryAsync(SalesSummaryRequest request, CancellationToken cancellationToken = default)
        {
            if (request.EndDate < request.StartDate)
            {
                throw new AppException(
                    code: "INVALID_DATE_RANGE",
                    message:
                        "End date must be greater than or equal to start date.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            var productCodes = request.ProductCodes
                .Where(code => code > 0)
                .Distinct()
                .ToArray();

            var summaries = await _salesRepository.GetSummaryAsync(
                 request.StartDate,
                 request.EndDate,
                productCodes,
                cancellationToken);

            return summaries
                .Select(summary => new SalesSummaryResponse
                {
                    ProductCode = summary.ProductCode,
                    ProductName = summary.ProductName,
                    TotalQuantity = summary.TotalQuantity,
                    TotalSales = summary.TotalSales
                })
                .ToArray();
        }
    }
}
