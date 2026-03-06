using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.UserSettings;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinanceTracker.Core.Services
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly ISessionService m_SessionService;
        private readonly FinanceTrackerContext m_FinanceTrackerContext;

        public UserSettingsService(ISessionService sessionService, FinanceTrackerContext financeTrackerContext)
        {
            m_SessionService = sessionService;
            m_FinanceTrackerContext = financeTrackerContext;
        }

        public async Task<TData> GetUserSettings<TData>(UserSettingCode code, ToolCode? toolCode = null, TileCode? tileCode = null) where TData : UserSetting
        {
            var userId = m_SessionService.CurrentUser;
            var hierarchyPath = HierarchyId.Parse(GetPath(toolCode, tileCode));
            var settings = await m_FinanceTrackerContext.UserSettings.FirstOrDefaultAsync(x => x.UserId == userId && x.HierarchyPath.Equals(hierarchyPath));
            if (settings == null || settings.SettingsJson == null)
                return Activator.CreateInstance<TData>();
            return JsonConvert.DeserializeObject<TData>(settings.SettingsJson) ?? Activator.CreateInstance<TData>();
        }

        public async Task SaveUserSetting(UserSettingCode userSetting, ToolCode? toolCode, TileCode? tileCode, UserSetting value)
        {
            if (value == null)
                return;
            var userId = m_SessionService.CurrentUser;
            var hierarchyPath = HierarchyId.Parse(GetPath(toolCode, tileCode));
            var savedSettings = await m_FinanceTrackerContext.UserSettings.FirstOrDefaultAsync(x => x.UserId == userId && x.HierarchyPath.Equals(hierarchyPath));
            if (savedSettings == null)
            {
                await m_FinanceTrackerContext.UserSettings.AddAsync(new Data.DBModels.UserSetting()
                {
                    UserId = userId,
                    HierarchyPath = hierarchyPath,
                    SettingCode = (int)userSetting,
                    ParentSettingCode = null,
                    SettingsJson = JsonConvert.SerializeObject(value)
                });
            }
            else
            {
                savedSettings.SettingsJson = JsonConvert.SerializeObject(value);
            }
            await m_FinanceTrackerContext.SaveChangesAsync();
        }

        private string GetPath(params Enum?[] nodes) => GetPath(nodes.Where(x => x != null).Select(x => Convert.ToInt32(x).ToString()).ToArray());
        private string GetPath(params string[] nodes) => nodes.Length == 0 ? "/" : $"/{string.Join("/", nodes)}/";
    }
}
