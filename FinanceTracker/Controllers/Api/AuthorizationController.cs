using FinanceTracker.Core.Models;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Authorization = FinanceTracker.Core.Services.Interfaces;

namespace FinanceTracker.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly Authorization.IAuthorizationService m_AuthorizationService;

        public AuthorizationController(Authorization.IAuthorizationService authorizationService)
        {
            m_AuthorizationService = authorizationService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel model)
        {
            if (!Request.Cookies.TryGetValue(CookieKeys.RefreshToken, out var refreshToken))
                return BadRequest();

            var token = await m_AuthorizationService.TryRefreshToken(model.AccessToken, refreshToken);
            if (token == null)
                return BadRequest();
            return Ok(new RefreshTokenModel() { AccessToken = token });
        }
    }
}
