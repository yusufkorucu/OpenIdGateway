namespace OpenIdGateway.Domain.RequestModels
{
    public class OpenIdRequestDto
    {
        public string ClientId { get; set; }
        public string SecretId { get; set; }
        public string TokenUrl { get; set; }
        public string RedirectUri { get; set; }
        public string UserInfoUrl { get; set; }
    }
}
