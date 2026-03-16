using FinanceTracker.Core.Models.LayoutEditor.EditorModels;

namespace FinanceTracker.Core.Models.LayoutEditor
{
    /// <summary>
    /// Used as layout of tile item element, stored in db
    /// </summary>
    public class LayoutEditorModel
    {
        public TileCode TileCode { get; set; }
        public List<FormControlData> FormControls { get; set; } = new();
        public LayoutEditorModel(TileCode tileCode)
        {
            TileCode = tileCode;
        }

    }
}
