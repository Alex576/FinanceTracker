using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Data.DBModels;

public partial class UserSetting
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int SettingCode { get; set; }

    public int? ParentSettingCode { get; set; }

    public string? SettingsJson { get; set; }

    public HierarchyId HierarchyPath { get; set; } = null!;

    public string? Hierarchy { get; set; }
}
