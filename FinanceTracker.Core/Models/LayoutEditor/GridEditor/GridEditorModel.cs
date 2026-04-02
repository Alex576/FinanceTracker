namespace FinanceTracker.Core.Models.LayoutEditor.GridEditor
{
    /// <summary>
    /// Stored in db
    /// </summary>
    public class GridEditorModel
    {
        public TileCode TileCode { get; set; }
        public GridEntity GridEntity { get; set; } = new();

        public GridEditorModel(TileCode tileCode)
        {
            TileCode = tileCode;
        }
    }
}
