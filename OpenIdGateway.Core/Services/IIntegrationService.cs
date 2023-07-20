using OpenIdGateway.Domain.GeneralResponseModel;
using OpenIdGateway.Domain.RequestModels;
using OpenIdGateway.Domain.ResponseModels;

namespace OpenIdGateway.Core.Services
{
    public interface IIntegrationService
    {
        Task<OpenIdGatewayApiResponse<OpenIdConnectResponseDto>> AuthAsync(OpenIdConnectRequestDto openIdConnectRequest);
    }
}
