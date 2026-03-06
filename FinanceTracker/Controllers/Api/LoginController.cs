using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Core.Models;
using System.IdentityModel.Tokens.Jwt;

namespace FinanceTracker.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService m_LoginService;

        public LoginController(ILoginService loginService)
        {
            m_LoginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<OperationResultData<UserModel>> Login(LoginModel model)
        {
            ArgumentNullException.ThrowIfNull(model.Login);
            ArgumentNullException.ThrowIfNull(model.Password);

            var (result, token) = await m_LoginService.Login(model.Login, model.Password);
            if (result.Code.IsSuccess() && token != null)
            {
                Response.Cookies.Append(CookieKeys.RefreshToken, new JwtSecurityTokenHandler().WriteToken(token), new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Path = "/",
                    Expires = token.ValidTo
                });
            }

            return result;
        }

        [HttpPost("[action]")]
        public async Task Logout(LogoutModel model)
        {
            await m_LoginService.Logout(model.Id);
            Response.Cookies.Delete(CookieKeys.RefreshToken);
        }
    }
}
