using FinanceTracker.Core.Models.LayoutEditor.DashboardEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Models.Dashboard
{
    public class DashboardItem
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<DashboardField> Fields { get; set; } = [];
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

        [JsonConstructor]
        public DashboardItem() { }

        public DashboardItem(DashboardItemOptions dashboardItemOptions)
        {
            Id = dashboardItemOptions.Id;
            X = dashboardItemOptions.X;
            Y = dashboardItemOptions.Y;
            Cols = dashboardItemOptions.Cols;
            Rows = dashboardItemOptions.Rows;
            Drag = dashboardItemOptions.Drag;
            Resize = dashboardItemOptions.Resize;
            MaxCols = dashboardItemOptions.MaxCols;
            MaxRows = dashboardItemOptions.MaxRows;
            MinCols = dashboardItemOptions.MinCols;
            MinRows = dashboardItemOptions.MinRows;
            Name = dashboardItemOptions.Data.Name;
            Fields.AddRange(dashboardItemOptions.Data.Fields.Select(x => new DashboardField() { Name = x.TileItemCode.ToString() }));
        }
    }

    public class DashboardField
    {
        public string? Name { get; set; }
        public JToken? Value { get; set; }
    }
}
