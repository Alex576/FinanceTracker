namespace FinanceTracker.Core.Models.Dashboard
{
    public class DashboardLayout
    {
        public DashboardOptions Options { get; set; } = new();
        public List<DashboardItem> Items { get; set; } = [];
    }
}
