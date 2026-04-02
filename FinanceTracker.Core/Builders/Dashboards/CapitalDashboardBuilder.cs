using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Core.Models.LayoutEditor.DashboardEditor;
using MasterData.Data.Models;

namespace FinanceTracker.Core.Builders.Dashboards
{
    public class CapitalDashboardBuilder : DashboardBuilder<FinanceModel>
    {
        public CapitalDashboardBuilder(DashboardEditorModel dashboardEditorModel) : base(dashboardEditorModel)
        {
        }

        protected override DashboardItem GetDashboardItem(FinanceModel dataItem)
        {
            var item = new DashboardItem();

            return item;
        }
    }
}
