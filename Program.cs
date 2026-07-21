
using Microsoft.EntityFrameworkCore;
using ZortouTest.DALs.ExternalServices;
using ZortouTest.DALs.Sales;
using ZortouTest.Data;
using ZortouTest.Services.Authen;
using ZortouTest.Services.Sales;

namespace ZortouTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddHttpClient<IZortApiClient, ZortApiClient>(client =>
            {
                var baseUrl = builder.Configuration["ZortApi:BaseUrl"];

                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    throw new InvalidOperationException(
                        """
        ZortApi:BaseUrl is not configured.
        Run:
        dotnet user-secrets set "ZortApi:BaseUrl" "<base-url>"
        """);
                }

                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            builder.Services.AddDbContext<ZortExamDbContext>(options =>
            {
                var connectionString =
                    builder.Configuration.GetConnectionString("ZortDatabase")
                    ?? throw new InvalidOperationException(
                        """
        ConnectionStrings:ZortDatabase is not configured.
        Run:
        dotnet user-secrets set "ConnectionStrings:ZortDatabase" "<connection-string>"
        """);

                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<ISalesRepository, SalesRepository>();
            builder.Services.AddScoped<ISalesService, SalesService>();


            var app = builder.Build();



            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
