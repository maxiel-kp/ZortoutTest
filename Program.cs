
using ZortouTest.DALs.ExternalServices;
using ZortouTest.Services;

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
                        "ZortApi:BaseUrl is missing from appsettings.json.");
                }

                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });


            var app = builder.Build();

            

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
