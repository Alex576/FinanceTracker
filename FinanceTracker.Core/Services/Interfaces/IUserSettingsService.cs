using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.UserSettings;
using FinanceTracker.Models;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface IUserSettingsService
    {
        Task<TData> GetUserSettings<TData>(UserSettingCode code, ToolCode? toolCode = null, TileCode? tileCode = null) where TData : UserSetting;
        Task SaveUserSetting(UserSettingCode userSetting, ToolCode? toolCode, TileCode? tileCode, UserSetting value);
    }
}