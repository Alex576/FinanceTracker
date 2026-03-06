using FinanceTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Core.Models
{
    public enum TileCode
    {
        Dashboard = 1,
        DashboardFilters = 2,
        DashboardDashboard = 3,
        FinancesLayout = 4,
        FinancesDashboard = 5,
        FinancesFilter = 6,
        FinancesGrid = 7,
        UsersLayout = 8,
        UsersFilter = 9,
        UsersGrid = 10,
        RoleLayout = 11,
        RoleFilter = 12,
        RoleGrid = 13,
        TranslationLayout = 14,
        TranslationFilter = 15,
        TranslationGrid = 16,
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
