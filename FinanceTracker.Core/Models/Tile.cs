using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models
{
    public enum TileCode
    {

    }

    public class Tile
    {
        public TileCode TileCode { get; set; }
        public ToolCode? ToolCode { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
        public TileTypeCode Type { get; set; }
        public HierarchyId Hierarchy { get; set; }
        public int? ParentTile { get; set; }

        public Tile() { }

        public Tile(FinanceTracker.Data.DBModels.Tile tile)
        {
            TileCode = (TileCode)tile.TileCode;
            ToolCode = (ToolCode?)tile.ToolCode;
            Name = tile.Name;
            Order = tile.Order;
            Type = (TileTypeCode)tile.Type;
            Hierarchy = tile.HierarchyPath;
            ParentTile = tile.ParentTileCode;
        }
    }
}
