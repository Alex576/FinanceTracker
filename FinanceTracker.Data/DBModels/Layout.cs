using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.DBModels;

public partial class Layout
{
    public int Id { get; set; }

    public string? LayoutJson { get; set; }

    public int TileCode { get; set; }

    public virtual Tile TileCodeNavigation { get; set; } = null!;
}
