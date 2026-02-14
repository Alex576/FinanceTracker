using FinanceTracker.Core.Models.OperationResult;
using Security.Core.Models;
using System.IdentityModel.Tokens.Jwt;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface ILoginService
    {
        Task<(OperationResult<UserModel> result, JwtSecurityToken? token)> Login(string login, string password);
    }
}