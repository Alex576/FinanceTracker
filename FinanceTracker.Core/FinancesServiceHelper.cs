using FinanceTracker.Controllers.Api;
using FinanceTracker.Core.Services;
using FinanceTracker.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Security.Core.Services;
using Security.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core
{
    public static class FinancesServiceHelper
    {
        public static void InitializeServices(IServiceCollection services)
        {
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IUserSettingsService, UserSettingsService>();
            services.AddScoped<IFormEditorService, FormEditorService>();
        }
    }
}
