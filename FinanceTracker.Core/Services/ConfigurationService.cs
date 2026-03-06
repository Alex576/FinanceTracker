using FinanceTracker.Core.Models;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Data.DBContext;
using Microsoft.Extensions.Options;

namespace FinanceTracker.Core.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private ConfigModel Config { get; }

        private readonly FinanceTrackerContext m_Context;

        public ConfigurationService(IOptions<ConfigModel> config, FinanceTrackerContext context)
        {
            Config = config.Value;
            m_Context = context;
        }

        public AppConfig GetApplicationConfig()
        {
            var config = new AppConfig();
            //var activeTool = m_Context.UserSettings.FindAsync(u => u.)
            config.Version = Config.Version;
            return config;
        }
    }
}
