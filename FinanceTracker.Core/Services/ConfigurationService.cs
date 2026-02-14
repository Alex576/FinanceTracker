using FinanceTracker.Controllers.Api;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Data.DBContext;
using FinanceTracker.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

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
