using FinanceTracker.Core.Models;
using FinanceTracker.Core.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Security.Data.DBModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinanceTracker.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly JWTModel m_JWTModel;

        public TokenService(IOptions<JWTModel> jwtOptions)
        {
            m_JWTModel = jwtOptions.Value;
        }

        public string GenerateAccessToken(User user)
        {
            var jwt = CreateToken(user, TimeSpan.FromMinutes(m_JWTModel.AccessTokenExpireInMinutes), m_JWTModel.SecretKey);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public JwtSecurityToken GenerateRefreshToken(User user)
        {
            var encodedJwt = CreateToken(user, TimeSpan.FromDays(m_JWTModel.RefreshTokenExpireInDays), m_JWTModel.SecretRefreshKey);
            return encodedJwt;
        }

        public async Task<bool> ValidateRefreshToken(string token)
        {
            var tokenHandler = new JsonWebTokenHandler();
            if (!tokenHandler.CanReadToken(token))
                return false;
            var result = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = m_JWTModel.Audience,
                ValidateIssuer = true,
                ValidIssuer = m_JWTModel.Issuer,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(m_JWTModel.SecretRefreshKey))
            });
            return result.IsValid;
        }

        private JwtSecurityToken CreateToken(User user, TimeSpan duration, string secretsKey)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
            };
            var startDate = DateTime.UtcNow;
            return new JwtSecurityToken(
                issuer: m_JWTModel.Issuer,
                audience: m_JWTModel.Audience,
                claims: claims,
                notBefore: startDate,
                expires: startDate.Add(duration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretsKey)), SecurityAlgorithms.HmacSha256)
                );
        }
    }
}
