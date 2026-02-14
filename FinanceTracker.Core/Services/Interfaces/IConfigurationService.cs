using FinanceTracker.Controllers.Api;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface IConfigurationService
    {
        AppConfig GetApplicationConfig();
    }
}