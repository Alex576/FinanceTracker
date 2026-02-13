using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.Models;

public partial class Tool
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? ToolCode { get; set; }

    public virtual ICollection<Tool> InverseToolCodeNavigation { get; set; } = new List<Tool>();

    public virtual ICollection<Tile> Tiles { get; set; } = new List<Tile>();

    public virtual Tool? ToolCodeNavigation { get; set; }
}
