using FinanceTracker.Data.Models;

namespace FinanceTracker.Core.Models.LayoutEntities
{
    public abstract class LayoutEntityBase
    {
        public TileCode TileCode { get; set; }
        public abstract TileTypeCode Type { get; }
        protected LayoutEntityBase(TileCode tileCode)
        {
            TileCode = tileCode;
        }

    }
}