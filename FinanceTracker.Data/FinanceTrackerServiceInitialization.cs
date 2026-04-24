using FinanceTracker.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.Data
{
    public class FinanceTrackerServiceInitialization
    {
        public static void InitializeServices(IServiceCollection services)
        {
            services.AddScoped<TileContextService>();
            services.AddScoped<LayoutContextService>();
            services.AddScoped<TranslationContextService>();
        }
    }
}
