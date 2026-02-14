using FinanceTracker.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Security.Data.DBContext;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace FinanceTracker.Core.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly SecurityContext m_SecurityContext;
        private readonly ITokenService m_TokenService;

        public AuthorizationService(SecurityContext securityContext, ITokenService tokenService)
        {
            m_SecurityContext = securityContext;
            m_TokenService = tokenService;
        }

        public async Task<string?> TryRefreshToken(string accessToken, string refreshToken)
        {
            if (!(await m_TokenService.ValidateRefreshToken(refreshToken)))
                return null;
            var user = await m_SecurityContext.Users.FirstOrDefaultAsync(x => !string.IsNullOrEmpty(x.RefreshToken) && x.RefreshToken.Equals(refreshToken));
            if (user == null)
                return null;

            return m_TokenService.GenerateAccessToken(user);
        }
    }
}
