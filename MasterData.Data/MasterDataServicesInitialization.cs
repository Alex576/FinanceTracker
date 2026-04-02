using MasterData.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MasterData.Data
{
    public static class MasterDataServicesInitialization
    {
        public static void InitializeServices(IServiceCollection services)
        {
            services.AddScoped<FinancesContextService>();
            services.AddScoped<ObjectContextService>();
            services.AddScoped<CapitalContextService>();
        }
    }
}
