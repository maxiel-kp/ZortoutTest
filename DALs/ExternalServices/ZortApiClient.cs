using System.Net.Http.Json;
using ZortouTest.DTOs.Authen;

namespace ZortouTest.DALs.ExternalServices
{
    public class ZortApiClient : IZortApiClient
    {
        private readonly HttpClient _httpClient;

        public ZortApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ZortLoginResult> LoginAsync(
            string username,
            string password,
            CancellationToken cancellationToken = default)
        {
            var request = new ZortLoginRequest
            {
                Username = username,
                Password = password
            };

            using var response = await _httpClient.PostAsJsonAsync(
                "api/Auth/Authenticate",
                request,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<ZortLoginResult>(
                    cancellationToken: cancellationToken);

            return result
                ?? throw new InvalidOperationException(
                    "Zort login response is empty.");
        }
    }
}
