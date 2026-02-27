using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.UserSettings
{
    public class LastSessionSetting : UserSetting
    {
        public ToolCode? LastOpenedTool { get; set; }
    }
}
