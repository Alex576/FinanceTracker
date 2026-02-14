using Security.Core.Models;
using Security.Data.DBModels;
using System.IdentityModel.Tokens.Jwt;

namespace FinanceTracker.Core.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        JwtSecurityToken GenerateRefreshToken(User user);
        Task<bool> ValidateRefreshToken(string token);
    }
}