namespace OpenIdGateway.Domain.GeneralResponseModel
{
    public interface IOpenIdGatewayApiResponse
    {
        bool IsSuccess { get; }
        string Message { get; }
        Exception Exception { get; }
    }

}
