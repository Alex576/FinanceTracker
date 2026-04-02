using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.LayoutPreviews;
using FinanceTracker.Data.Services;

namespace FinanceTracker.Core.Builders.Layouts
{
    public abstract class BaseFormLayoutBuilder
    {
        protected readonly TileContextService m_FinanceTrackerContext;
        private int _index = 0;

        protected BaseFormLayoutBuilder(TileContextService financeTrackerContext)
        {
            m_FinanceTrackerContext = financeTrackerContext;
        }

        public abstract Task<LayoutPreview> GetLayoutAsync(ToolCode toolCode);
        public abstract Task<LayoutPreview> GetLayoutAsync(List<Tile> layoutTiles);

        //public string GetControlId(TileItemCode tileItemCode)
        //{
        //    return ItemCodeHelper.GetItemCode(tileItemCode, _index++);
        //}
    }
}
