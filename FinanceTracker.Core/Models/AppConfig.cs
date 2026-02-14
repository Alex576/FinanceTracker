using FinanceTracker.Core.Models;

namespace FinanceTracker.Controllers.Api
{
    public class AppConfig
    {
        public string? Version { get; set; }
        public ToolCode ActiveTool { get; set; }
    }
}