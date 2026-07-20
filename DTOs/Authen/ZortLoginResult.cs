using System.Text.Json.Serialization;

namespace ZortouTest.DTOs.Authen
{
    public class ZortLoginResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public ZortLoginData? Data { get; set; }

        [JsonPropertyName("error")]
        public ZortApiError? Error { get; set; }
    }

    public class ZortLoginData
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("expiry")]
        public DateTimeOffset Expiry { get; set; }
    }

    public class ZortApiError
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
