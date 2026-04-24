namespace FinanceTracker.Core.Models.LayoutEditor.DashboardEditor
{
    /// <summary>
    /// Stored in db
    /// </summary>
    public class DashboardEditorModel : ItemEditorModelBase
    {
        public List<DashboardItemOptions> Items { get; set; } = [];

        public DashboardEditorModel(TileCode tileCode) : base(tileCode) { }
    }
}
