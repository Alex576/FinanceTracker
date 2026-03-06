using FinanceTracker.Data.Models;

namespace FinanceTracker.Core.Models.LayoutEntities
{
    public class LayoutEntity
    {
        public TileCode TileCode { get; set; }
        public LayoutEntityBase Data { get; set; }
    }
}
