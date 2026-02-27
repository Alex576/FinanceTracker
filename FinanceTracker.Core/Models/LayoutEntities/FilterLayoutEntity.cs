using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.LayoutEntities
{
    public class FilterLayoutEntity : LayoutEntityBase
    {
        //public override TileTypeCode Type => TileTypeCode.Filter;
        public List<FilterControlEntity> Filters { get; set; } = new();

    }
}
