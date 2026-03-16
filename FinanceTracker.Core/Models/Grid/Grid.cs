namespace FinanceTracker.Core.Models.Grid
{
    public class Grid
    {
        public Layout Layout { get; set; } = new();
        public List<Row> Rows { get; set; } = new();
    }
}
