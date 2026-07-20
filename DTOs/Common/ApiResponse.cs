using ZortouTest.DTOs.Authen;

namespace ZortouTest.DTOs.Common
{
    public class ApiResponse<T>
    {

        public bool Success { get; init; }

        public T? Data { get; init; }

        public ApiError? Error { get; init; }

        public static ApiResponse<T> Ok(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Error = null
            };
        }

        public static ApiResponse<T> Fail(
            string code,
            string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default,
                Error = new ApiError
                {
                    Code = code,
                    Message = message
                }
            };
        }
    }

    public sealed class ApiError
    {
        public string Code { get; init; } = string.Empty;

        public string Message { get; init; } = string.Empty;
    }
}
