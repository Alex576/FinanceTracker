using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.LayoutEntities
{
    public class LayoutEntity
    {
        public TileTypeCode Type { get; set; }
        public TileCode TileCode { get; set; }
        public LayoutEntityBase Data { get; set; }
    }
}
