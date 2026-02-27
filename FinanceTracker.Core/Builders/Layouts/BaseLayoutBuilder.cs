using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.LayoutPreviews;
using FinanceTracker.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace FinanceTracker.Core.Builders.Layouts
{
    public abstract class BaseLayoutBuilder
    {
        public abstract Task<LayoutPreview> GetLayoutAsync(ToolCode toolCode, FinanceTrackerContext financeTrackerContext);
        public abstract Task<LayoutPreview> GetLayoutAsync(List<Tile> layoutTiles, FinanceTrackerContext financeTrackerContext);
    }
}
