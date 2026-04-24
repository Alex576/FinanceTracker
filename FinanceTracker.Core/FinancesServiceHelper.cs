using FinanceTracker.Core.Cache;
using FinanceTracker.Core.Services;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Data;
using MasterData.Data;
using MasterData.Data.Storages;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.Core
{
    public static class FinancesServiceHelper
    {
        public static void InitializeServices(IServiceCollection services)
        {
            services.AddSingleton<ICache, Cache.Cache>();
            services.AddSingleton<IMasterDataCache>((provider) => provider.GetRequiredService<ICache>());

            MasterDataServicesInitialization.InitializeServices(services);
            FinanceTrackerServiceInitialization.InitializeServices(services);
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<ITranslationService, TranslationService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IUserSettingsService, UserSettingsService>();
            services.AddScoped<ILayoutItemService, LayoutItemService>();
            services.AddScoped<IGridEditorService, GridEditorService>();
            services.AddScoped<ICapitalService, CapitalService>();
            services.AddScoped<IFinancesService, FinancesService>();
        }
    }
}
