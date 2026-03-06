using Microsoft.Extensions.DependencyInjection;
using Security.Core.Services;
using Security.Core.Services.Interfaces;

namespace Security.Core
{
    public static class SecurityServiceHelper
    {
        public static void InitializeServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
