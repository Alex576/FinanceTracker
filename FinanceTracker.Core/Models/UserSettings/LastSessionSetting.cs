namespace FinanceTracker.Core.Models.UserSettings
{
    public class LastSessionSetting : UserSetting
    {
        public override UserSettingCode SettingCode => UserSettingCode.LastOpenedTool;
        public ToolCode? LastOpenedTool { get; set; }
    }
}
