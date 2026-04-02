namespace FinanceTracker.Core.Models.LayoutEditor.DashboardEditor
{
    public class DashboardItemOptions
    {
        public DashboardMasterData Data { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Cols { get; set; }
        public int Rows { get; set; }
        public bool Drag { get; set; }
        public bool Resize { get; set; }
        public int? MaxCols { get; set; }
        public int? MaxRows { get; set; }
        public int? MinCols { get; set; }
        public int? MinRows { get; set; }
    }

    public class DashboardMasterData
    {
        public string Name { get; set; }
        public List<DashboardItemField> Fields { get; set; } = [];
    }

    public class DashboardItemField
    {
        public TileItemCode TileItemCode { get; set; }

    }
}
