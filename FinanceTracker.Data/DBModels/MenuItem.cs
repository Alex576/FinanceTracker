using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.DBModels;

public partial class MenuItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentMenuCode { get; set; }

    public int ToolCode { get; set; }

    public virtual ICollection<MenuItem> InverseParentMenuCodeNavigation { get; set; } = new List<MenuItem>();

    public virtual MenuItem? ParentMenuCodeNavigation { get; set; }

    public virtual Tool ToolCodeNavigation { get; set; } = null!;
}
