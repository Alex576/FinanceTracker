namespace FinanceTracker.Core.Models.LayoutEditor.GridEditor
{
    /// <summary>
    /// Stored in db
    /// </summary>
    public class GridEditorModel : ItemEditorModelBase
    {
        public GridEntity GridEntity { get; set; } = new();

        public GridEditorModel(TileCode tileCode) : base(tileCode) { }
    }
}
