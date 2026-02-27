using FinanceTracker.Core.Models.UserSettings;

namespace FinanceTracker.Models
{
    public class SaveUserSettingsModel<T>: UserSettingsModel where T : UserSetting
    {
        public T Value { get; set; } = null!;
    }
}
