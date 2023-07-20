namespace OpenIdGateway.Domain.ResponseModels
{
    public class OpenIdConnectResponseDto
    {
        public TokenResponseDto TokenResponse { get; set; }
        public UserInfoResponseDto UserInfoResponse { get; set; }
    }
}
