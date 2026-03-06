using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services.Interfaces;
using Security.Core.Models;
using Security.Core.Services.Interfaces;
using Security.Data.DBModels;
using System.IdentityModel.Tokens.Jwt;

namespace FinanceTracker.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserService m_UserService;
        private readonly ITokenService m_TokenService;

        public LoginService(IUserService userService, ITokenService tokenService)
        {
            m_UserService = userService;
            m_TokenService = tokenService;
        }

        public async Task<(OperationResultData<UserModel> result, JwtSecurityToken? token)> Login(string login, string password)
        {
            var user = await m_UserService.GetUser(login, password);

            if (user == null)
                return (new OperationResultData<UserModel>(null, ResultCode.Error, "Failed to login"), null);
            else
            {
                var refreshToken = await UpdateRefreshToken(user);
                return (new OperationResultData<UserModel>(new UserModel(user) { AccessToken = GenerateAccessToken(user) }, ResultCode.Success, "Login success"), refreshToken);
            }
        }

        public async Task Logout(int id)
        {
            await m_UserService.UpdateUser(id, x => x.SetProperty(p => p.RefreshToken, v => null));
        }

        private string GenerateAccessToken(User user) => m_TokenService.GenerateAccessToken(user);
        private async Task<JwtSecurityToken> UpdateRefreshToken(User user)
        {
            var jwt = m_TokenService.GenerateRefreshToken(user);
            await m_UserService.SetUserToken(user, new JwtSecurityTokenHandler().WriteToken(jwt));
            return jwt;
        }
    }
}
