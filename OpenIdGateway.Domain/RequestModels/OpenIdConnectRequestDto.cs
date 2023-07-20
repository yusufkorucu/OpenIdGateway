namespace OpenIdGateway.Domain.RequestModels
{
    public class OpenIdConnectRequestDto
    {
        public string OpenIdCode { get; set; }
        public OpenIdRequestDto OpenIdRequestDto { get; set; }
    }
}
