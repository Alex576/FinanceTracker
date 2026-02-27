using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.LayoutPreviews;
using FinanceTracker.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Core.Builders.Layouts
{
    public class LayoutEditorBuilder : BaseLayoutBuilder
    {
        public override async Task<LayoutPreview> GetLayoutAsync(ToolCode toolCode, FinanceTrackerContext financeTrackerContext)
        {
            var layoutPreview = new LayoutPreview();
            var tileLayout = await financeTrackerContext.Tiles.FirstOrDefaultAsync(x => x.ToolCode == (int)toolCode && x.Type == (int)TileTypeCode.Layout);
            if (tileLayout == null)
                return layoutPreview;


            return await GetLayoutAsync([new(tileLayout)], financeTrackerContext);
        }

        public override async Task<LayoutPreview> GetLayoutAsync(List<Tile> layoutTiles, FinanceTrackerContext financeTrackerContext)
        {
            var layoutPreview = new LayoutPreview();
            foreach (var tile in layoutTiles.Where(x => x.Type == TileTypeCode.Layout))
            {
                var layoutItems = await financeTrackerContext.Tiles.Where(x => x.TileCode != (int)tile.TileCode && x.HierarchyPath.IsDescendantOf(tile.Hierarchy)).Select(x => new Tile(x)).ToListAsync();
                foreach (var layoutItem in layoutItems.OrderBy(x => x.Order ?? 0))
                {
                    switch (layoutItem.Type)
                    {
                        case TileTypeCode.Dashboard:
                            layoutPreview.Previews.Add(new DashboardPreview(layoutItem.TileCode));
                            break;
                        case TileTypeCode.Grid:
                            layoutPreview.Previews.Add(new GridPreview(layoutItem.TileCode));
                            break;
                        case TileTypeCode.Form:
                            break;
                        case TileTypeCode.Filter:
                            layoutPreview.Previews.Add(new FilterPreview(layoutItem.TileCode));
                            break;
                        case TileTypeCode.Layout:
                            break;
                        default:
                            break;
                    }
                }
            }
            return layoutPreview;
        }
    }
}
