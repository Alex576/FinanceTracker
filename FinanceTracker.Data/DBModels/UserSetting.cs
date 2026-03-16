using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.DBModels;

public partial class UserSetting
{
    public int Id { get; set; }

    public string Path { get; set; } = null!;

    public int UserId { get; set; }

    public int SettingCode { get; set; }

    public int? ParentSettingCode { get; set; }

    public string? SettingsJson { get; set; }
}
