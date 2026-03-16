using FinanceTracker.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Data
{
    public class FinanceTrackerServiceInitialization
    {
        public static void InitializeServices(IServiceCollection services)
        {
            services.AddScoped<TileContextService>();
            services.AddScoped<LayoutContextService>();
        }
    }
}
