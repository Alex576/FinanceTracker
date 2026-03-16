using FinanceTracker.Core.Services;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Data;
using FinanceTracker.Data.Services;
using MasterData.Data;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.Core
{
    public static class FinancesServiceHelper
    {
        public static void InitializeServices(IServiceCollection services)
        {
            MasterDataServicesInitialization.InitializeServices(services);
            FinanceTrackerServiceInitialization.InitializeServices(services);
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IUserSettingsService, UserSettingsService>();
            services.AddScoped<ILayoutItemService, FilterEditorService>();
            services.AddScoped<IGridEditorService, GridEditorService>();
            services.AddScoped<IFinancesService, FinancesService>();
        }
    }
}
