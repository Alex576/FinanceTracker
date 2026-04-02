using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Core.Models.LayoutEditor.DashboardEditor;

namespace FinanceTracker.Core.Builders.Dashboards
{
    public abstract class DashboardBuilder<TData> where TData : class
    {
        private DashboardEditorModel m_DashboardEditorModel;

        protected DashboardBuilder(DashboardEditorModel dashboardEditorModel)
        {
            m_DashboardEditorModel = dashboardEditorModel;
        }

        public DashboardLayout GetDashboardLayout(List<TData> data)
        {
            var layout = new DashboardLayout();
            foreach (var dataItem in data)
                layout.Items.Add(GetDashboardItem(dataItem));

            layout.Options.CanAdd = true;
            return layout;
        }

        protected abstract DashboardItem GetDashboardItem(TData dataItem);
    }
}
