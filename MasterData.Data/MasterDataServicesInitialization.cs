using MasterData.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.Data
{
    public static class MasterDataServicesInitialization
    {
        public static void InitializeServices(IServiceCollection services)
        {
            services.AddScoped<FinancesContextService>();
            services.AddScoped<CapitalContextService>();
        }
    }
}
