using FinanceTracker.Core.Models.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.UserSettings
{
    public class FilterValueSettings : UserSetting
    {
        public override UserSettingCode SettingCode => UserSettingCode.FinancesFilterSettings;
        public List<ControlValue> Values { get; set; } = new();
    }
}
