using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.LayoutEntities
{
    /// <summary>
    /// Used in layout editor
    /// </summary>
    public class DashboardLayoutEntity : LayoutEntityBase
    {
        public override TileTypeCode Type => TileTypeCode.Dashboard;
        public DashboardLayout DashboardLayout { get; set; } = new();
        public DashboardLayoutEntity(TileCode tileCode) : base(tileCode)
        {
        }
    }
}
