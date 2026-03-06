using FinanceTracker.Core.Models.UserSettings;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingsController : ControllerBase
    {
        private readonly IUserSettingsService m_UserSettingsService;

        public UserSettingsController(IUserSettingsService userSettingsService)
        {
            m_UserSettingsService = userSettingsService;
        }

        [HttpPost("[action]")]
        public async Task<UserSetting> GetUserSettings(UserSettingsModel model)
        {
            return await m_UserSettingsService.GetUserSettings<UserSetting>(model.SettingCode, model.ToolCode, model.TileCode);
        }

        [HttpPost("[action]")]
        public async Task<LastSessionSetting> GetLastSessionSetting(UserSettingsModel model)
        {
            return await m_UserSettingsService.GetUserSettings<LastSessionSetting>(model.SettingCode, model.ToolCode, model.TileCode);
        }

        [HttpPost("[action]")]
        public async Task SaveUserLastSessionSetting(SaveUserSettingsModel<LastSessionSetting> model)
        {
            await m_UserSettingsService.SaveUserSetting(model.SettingCode, model.ToolCode, model.TileCode, model.Value);
        }
    }
}
