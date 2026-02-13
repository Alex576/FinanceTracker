using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.Models;

public partial class TileType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Tile> Tiles { get; set; } = new List<Tile>();
}
