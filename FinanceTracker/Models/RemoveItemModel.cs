using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.LayoutEditor;

namespace FinanceTracker.Models
{
    public class RemoveItemModel
    {
        public TileCode TileCode { get; set; }
        public string ItemId { get; set; }
        public EditorType Type { get; set; }
    }
}
