using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.LayoutEntities;

namespace FinanceTracker.Core.Models.Layout
{
    public class LayoutEditor
    {
        public FormControl TileFilter { get; set; }
        public List<LayoutEntity> LayoutItems { get; set; }

    }
}