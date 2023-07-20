namespace OpenIdGateway.Domain.GeneralResponseModel
{
    public class OpenIdGatewayApiResponse<T> : IOpenIdGatewayApiResponse
    {

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public T Data { get; set; }

        public OpenIdGatewayApiResponse()
        {

        }

        public OpenIdGatewayApiResponse(bool isSuccess, T data)
        {
            IsSuccess = isSuccess;
            Data = data;
        }

        public OpenIdGatewayApiResponse(bool isSuccess, T data, string message)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;

        }

        public OpenIdGatewayApiResponse(bool isSuccess, Exception exception)
        {
            IsSuccess = isSuccess;
            Exception = exception;
        }

        public OpenIdGatewayApiResponse(bool isSuccess, string message, Exception exception)
        {
            IsSuccess = isSuccess;
            Message = message;
            Exception = exception;
        }
        public OpenIdGatewayApiResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public OpenIdGatewayApiResponse(bool isSuccess, List<string> messages)
        {
            IsSuccess = isSuccess;
            Message = string.Join(",", messages);
        }

    }
}
