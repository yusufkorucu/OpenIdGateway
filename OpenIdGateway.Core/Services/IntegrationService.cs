using FluentValidation;
using OpenIdGateway.Core.CoreMessage;
using OpenIdGateway.Core.Helper;
using OpenIdGateway.Domain.GeneralResponseModel;
using OpenIdGateway.Domain.RequestModels;
using OpenIdGateway.Domain.ResponseModels;
using RestSharp;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace OpenIdGateway.Core.Services
{
    public class IntegrationService : IIntegrationService
    {
        #region Field

        private readonly IValidator<OpenIdConnectRequestDto> _validator;
        private readonly ILogger<IntegrationService> _logger;
        #endregion

        #region Ctor

        public IntegrationService(IValidator<OpenIdConnectRequestDto> validator, ILogger<IntegrationService> logger)
        {
            _validator = validator;
            _logger = logger;
        }

        #endregion


        #region Methods

        public async Task<OpenIdGatewayApiResponse<OpenIdConnectResponseDto>> AuthAsync(OpenIdConnectRequestDto openIdConnectRequest)
        {
            var modelIsValid = await _validator.ValidateAsync(openIdConnectRequest);

            if (!modelIsValid.IsValid)
                return new OpenIdGatewayApiResponse<OpenIdConnectResponseDto>(false, modelIsValid.Errors.Select(x => x.ErrorMessage).ToList());

            try
            {
                var encodedCredentails = Convert.ToBase64String(Encoding.Default.GetBytes(openIdConnectRequest.OpenIdRequestDto.ClientId + ":" + openIdConnectRequest.OpenIdRequestDto.SecretId));

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var client = new RestClient();
                client = new RestClient(openIdConnectRequest.OpenIdRequestDto.TokenUrl);
                var restRequest = new RestRequest();

                restRequest.AddHeader("Authorization", "Basic" + encodedCredentails);
                restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                restRequest.AddParameter("grant_type", "authorization_code");
                restRequest.AddParameter("code", openIdConnectRequest.OpenIdCode);
                restRequest.AddParameter("redirect_uri", openIdConnectRequest.OpenIdRequestDto.RedirectUri);

                var tokenResponse = await client.ExecutePostAsync<TokenResponseDto>(restRequest);

                var requestJson = JsonSerializer.Serialize(openIdConnectRequest);

                var logItem = LogHelper.CreateLogItem(request: requestJson, response: tokenResponse?.Content, clientId: openIdConnectRequest.OpenIdRequestDto.ClientId, methodName: "OpenIdToken",secretId: openIdConnectRequest.OpenIdRequestDto.SecretId);

                _logger.LogInformation(logItem);

                if (tokenResponse?.Data?.AccessToken != null && tokenResponse?.Data?.IdToken != null)
                {
                    client = new RestClient(openIdConnectRequest.OpenIdRequestDto.UserInfoUrl);

                    restRequest = new RestRequest();
                    restRequest.AddHeader("Authorization", "Bearer " + tokenResponse.Data.AccessToken);

                    var userInfoResponse = await client.ExecuteGetAsync<UserInfoResponseDto>(restRequest);

                    var userInfoLogItem = LogHelper.CreateLogItem(request: requestJson, response: tokenResponse?.Content, clientId: openIdConnectRequest.OpenIdRequestDto.ClientId, methodName: "OpenIdUserInfo", secretId: openIdConnectRequest.OpenIdRequestDto.SecretId);

                    _logger.LogInformation(logItem);

                    if (userInfoResponse?.Data != null)
                    {
                        var responseDto = new OpenIdConnectResponseDto
                        {
                            TokenResponse = tokenResponse.Data,
                            UserInfoResponse = userInfoResponse.Data
                        };

                        return new OpenIdGatewayApiResponse<OpenIdConnectResponseDto>(isSuccess: true, responseDto);
                    }

                    return new OpenIdGatewayApiResponse<OpenIdConnectResponseDto>(isSuccess: false, message: string.Format(ServiceMessage.UserInfoErrorMessage, userInfoResponse?.Content + userInfoResponse?.ErrorMessage));

                }

                return new OpenIdGatewayApiResponse<OpenIdConnectResponseDto>(isSuccess: false, message: string.Format(ServiceMessage.TokenErrorMessage, tokenResponse?.ErrorMessage + tokenResponse?.Content));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new OpenIdGatewayApiResponse<OpenIdConnectResponseDto>(isSuccess: false, exception: ex);

            }

        }

        #endregion

    }
}
