using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using ZortouTest.Data;
using ZortouTest.Models.Entities;
using ZortouTest.Models.Temps;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ZortouTest.DALs.Sales
{
    public class SalesRepository : ISalesRepository
    {
        private readonly ZortExamDbContext _dbContext;

        public SalesRepository(ZortExamDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<SalesSummaryModel>> GetSummaryAsync(DateTime startDate, DateTime endExclusive, IReadOnlyCollection<int> productCodes, CancellationToken cancellationToken = default)
        {
            var query =
            from orderDetail in _dbContext.OrderDetails.AsNoTracking()

            join orderProduct in _dbContext.OrderProducts.AsNoTracking()
                on (Guid?)orderDetail.OrderNumber
                equals orderProduct.OrderNumber

            join product in _dbContext.Products.AsNoTracking()
                on orderProduct.ProductCode
                equals (int?)product.ProductCode

            where orderDetail.CreatedDate.HasValue
               && orderDetail.CreatedDate.Value >= startDate
               && orderDetail.CreatedDate.Value < endExclusive

            select new
            {
                product.ProductCode,
                product.ProductName,
                Quantity = orderProduct.Quantity ?? 0,
                PricePerUnit = orderProduct.PricePerUnit ?? 0m
            };

            if (productCodes.Count > 0)
            {
                query = query.Where(x =>
                    productCodes.Contains(x.ProductCode));
            }

            return await query
                .GroupBy(x => new
                {
                    x.ProductCode,
                    x.ProductName
                })
                .Select(group => new SalesSummaryModel
                {
                    ProductCode = group.Key.ProductCode,
                    ProductName = group.Key.ProductName,

                    TotalQuantity = group.Sum(x =>
                        x.Quantity),

                    TotalSales = group.Sum(x =>
                        x.PricePerUnit * x.Quantity)
                })
                .OrderBy(x => x.ProductCode)
                .ToListAsync(cancellationToken);
        }
    }
}
