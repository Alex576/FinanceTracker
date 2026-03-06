using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Data.DBModels;

public partial class Tile
{
    public int TileCode { get; set; }

    public string Name { get; set; } = null!;

    public int? ToolCode { get; set; }

    public int? Order { get; set; }

    public int Type { get; set; }

    public int? ParentTileCode { get; set; }

    public HierarchyId? HierarchyPath { get; set; }

    public string? Hierarchy { get; set; }

    public virtual ICollection<Layout> Layouts { get; set; } = new List<Layout>();

    public virtual Tool? ToolCodeNavigation { get; set; }

    public virtual TileType TypeNavigation { get; set; } = null!;
}
