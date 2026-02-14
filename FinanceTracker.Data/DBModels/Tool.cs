using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.DBModels;

public partial class Tool
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentToolCode { get; set; }

    public virtual ICollection<Tool> InverseParentToolCodeNavigation { get; set; } = new List<Tool>();

    public virtual Tool? ParentToolCodeNavigation { get; set; }

    public virtual ICollection<Tile> Tiles { get; set; } = new List<Tile>();

    public virtual ICollection<UserSetting> UserSettings { get; set; } = new List<UserSetting>();
}
