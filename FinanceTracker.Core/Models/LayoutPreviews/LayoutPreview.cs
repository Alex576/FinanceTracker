using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.LayoutPreviews
{
    public class LayoutPreview
    {
        public List<BasePreviewItem> Previews { get; set; } = new();
        //public List<FilterPreview> FilterPreviews { get; set; } = new();
        //public List<GridPreview> GridPreviews { get; set; } = new();
        //public List<DashboardPreview> DashboardPreviews { get; set; } = new();
    }
}
