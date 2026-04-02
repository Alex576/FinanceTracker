using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;

namespace FinanceTracker.Models
{
    public class LayoutItemFormSaveEditorModel
    {
        public TileCode TileCode { get; set; }
        public string? ItemId { get; set; }
        public EditorType Type { get; set; }
        public SaveFormModel Value { get; set; }
    }
}
