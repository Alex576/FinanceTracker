using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.DBModels;

public partial class Tile
{
    public int Id { get; set; }

    public int TileId { get; set; }

    public string Name { get; set; } = null!;

    public int? ToolId { get; set; }

    public int? Order { get; set; }

    public int Type { get; set; }

    public int? ParentTileId { get; set; }

    public virtual ICollection<Tile> InverseParentTile { get; set; } = new List<Tile>();

    public virtual ICollection<Layout> Layouts { get; set; } = new List<Layout>();

    public virtual Tile? ParentTile { get; set; }

    public virtual Tool? Tool { get; set; }

    public virtual TileType TypeNavigation { get; set; } = null!;

    public virtual ICollection<UserSetting> UserSettings { get; set; } = new List<UserSetting>();
}
