using FinanceTracker.Core.Models;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface IConfigurationService
    {
        AppConfig GetApplicationConfig();
    }
}