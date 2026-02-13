using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.Models;

public partial class Layout
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? LayoutJson { get; set; }

    public int? TileId { get; set; }

    public virtual Tile? Tile { get; set; }
}
