namespace FinanceTracker.Core.Models.LayoutEditor.DashboardEditor
{
    /// <summary>
    /// Stored in db
    /// </summary>
    public class DashboardEditorModel
    {
        public TileCode TileCode { get; set; }
        public List<DashboardItemOptions> Items { get; set; } = [];

        public DashboardEditorModel(TileCode tileCode)
        {
            TileCode = tileCode;
        }
    }
}
