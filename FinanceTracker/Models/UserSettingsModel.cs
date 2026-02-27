using FinanceTracker.Core.Models;

namespace FinanceTracker.Models
{
    public class UserSettingsModel
    {
        public UserSettingCode SettingCode { get; set; }
        public ToolCode? ToolCode { get; set; }
        public TileCode? TileCode { get; set; }
    }
}
