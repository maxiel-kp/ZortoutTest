namespace ZortouTest.DALs.ExternalServices
{
    public class AppException : Exception
    {
        public string Code { get; }
        public int StatusCode { get; }

        public AppException(
            string code,
            string message,
            int statusCode)
            : base(message)
        {
            Code = code;
            StatusCode = statusCode;
        }
    }
}
