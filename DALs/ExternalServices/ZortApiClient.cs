using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using ZortouTest.DTOs.Authen;

namespace ZortouTest.DALs.ExternalServices
{
    public class ZortApiClient : IZortApiClient
    {
        private readonly HttpClient _httpClient;
        ILogger<ZortApiClient> _logger;

        public ZortApiClient(HttpClient httpClient, ILogger<ZortApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        async Task<IEnumerable<ZortCostResult>> IZortApiClient.GetCostsAsync(string bearerToken, DateTime startDate, DateTime endDate, int[] productCodes, CancellationToken cancellationToken)
        {
            var parameters = new List<string>
        {
            $"startDate={Uri.EscapeDataString(startDate.ToString("yyyy-MM-dd"))}",
            $"endDate={Uri.EscapeDataString(endDate.ToString("yyyy-MM-dd"))}"
        };

            parameters.AddRange(
                productCodes.Select(code => $"productCodes={code}"));

            var requestUri = $"api/cost?{string.Join("&", parameters)}";

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                requestUri);

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    bearerToken);

            using var response = await _httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            var responseBody = await response.Content.ReadAsStringAsync(
                cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new AppException(
                    code: "INVALID_TOKEN",
                    message: "Bearer token is invalid or expired.",
                    statusCode: StatusCodes.Status401Unauthorized);
            }

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new AppException(
                    code: "ACCESS_DENIED",
                    message: "The token does not have permission to access cost data.",
                    statusCode: StatusCodes.Status403Forbidden);
            }

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Zort Cost API failed with status {StatusCode}. Response: {Response}",
                    (int)response.StatusCode,
                    responseBody);

                throw new AppException(
                    code: "ZORT_COST_API_FAILED",
                    message: "Unable to retrieve cost information from Zort service.",
                    statusCode: StatusCodes.Status502BadGateway);
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var payload =
                JsonSerializer.Deserialize<
                    ZortApiResponse<List<ZortCostApiItem>>>(
                    responseBody,
                    options);

            if (payload is null)
            {
                throw new AppException(
                    code: "INVALID_COST_RESPONSE",
                    message: "Zort service returned an invalid response.",
                    statusCode: StatusCodes.Status502BadGateway);
            }

            if (!payload.Success)
            {
                throw new AppException(
                    code: payload.Error?.Code ?? "ZORT_COST_API_FAILED",
                    message: payload.Error?.Message
                             ?? "Unable to retrieve cost information.",
                    statusCode: StatusCodes.Status502BadGateway);
            }

            return payload.Data?
                .Select(item => new ZortCostResult
                {
                    ProductCode = item.ProductCode,
                    Cost = item.Cost
                })
                .ToArray()
                ?? [];
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
