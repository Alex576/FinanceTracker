using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.DBModels;

public partial class UserSetting
{
    public int Id { get; set; }

    public int ToolId { get; set; }

    public int? TileId { get; set; }

    public string? OptionsJson { get; set; }

    public int UserId { get; set; }

    public virtual Tile? Tile { get; set; }

    public virtual Tool Tool { get; set; } = null!;
}
