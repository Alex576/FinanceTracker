using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.DBModels;

public partial class TileType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Tile> Tiles { get; set; } = new List<Tile>();
}
