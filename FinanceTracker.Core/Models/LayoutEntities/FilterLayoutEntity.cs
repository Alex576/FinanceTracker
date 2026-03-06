using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Data.Models;

namespace FinanceTracker.Core.Models.LayoutEntities
{
    public class FilterLayoutEntity : LayoutEntityBase
    {
        public override TileTypeCode Type => TileTypeCode.Filter;
        public List<FormControl> Filters { get; set; } = new();
        public FilterLayoutEntity(TileCode tileCode) : base(tileCode)
        { }

    }
}
