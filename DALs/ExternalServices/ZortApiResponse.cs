using System.Text.Json.Serialization;
using ZortouTest.DTOs.Authen;

namespace ZortouTest.DALs.ExternalServices
{
    public class ZortApiResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; init; }
        [JsonPropertyName("error")]
        public ZortApiError? Error { get; init; }

        [JsonPropertyName("data")]
        public T? Data { get; init; }
    }

    public class ZortApiError
    {
        [JsonPropertyName("code")]
        public string? Code { get; init; }
        [JsonPropertyName("message")]
        public string? Message { get; init; }
    }
}