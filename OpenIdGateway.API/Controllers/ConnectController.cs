using Microsoft.AspNetCore.Mvc;
using OpenIdGateway.Core.Services;
using OpenIdGateway.Domain.RequestModels;

namespace OpenIdGateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectController : ControllerBase
    {

        #region Field

        private IIntegrationService _integrationService;

        #endregion

        #region Ctor

        public ConnectController(IIntegrationService integrationService)
        {
            _integrationService = integrationService;
        }

        #endregion

        #region Methods

        [HttpPost("Login")]
        public async Task<IActionResult> Login(OpenIdConnectRequestDto requestDto)
        {

            var result = await _integrationService.AuthAsync(requestDto);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }

        #endregion
    }
}
