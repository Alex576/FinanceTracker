using FinanceTracker.Core.Builders.Dashboards;
using FinanceTracker.Core.Builders.Filter;
using FinanceTracker.Core.Builders.Grids;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Core.Models.Finances;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.DashboardEditor;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.Services;
using MasterData.Data.Services;

namespace FinanceTracker.Core.Services
{
    public class FinancesService : IFinancesService
    {
        private readonly LayoutContextService m_LayoutContextService;
        private readonly FinancesContextService m_FinancesContextService;
        private readonly ObjectContextService m_ObjectContextService;

        public FinancesService(LayoutContextService layoutContextService, FinancesContextService financesContextService, ObjectContextService objectContextService)
        {
            m_LayoutContextService = layoutContextService;
            m_FinancesContextService = financesContextService;
            m_ObjectContextService = objectContextService;
        }

        public async Task<List<FormControl>> GetFilters(ToolCode toolCode)
        {
            var tileCode = toolCode.GetFilterTileCode();
            var filterLayout = await m_LayoutContextService.GetLayout<LayoutEditorModel>((int)tileCode);
            if (filterLayout == null)
                return new List<FormControl>();

            var filterBuilder = new FinancesFilterBuilder(filterLayout.FormControls);
            var finances = await m_FinancesContextService.GetAllFinances();
            var model = new FinanceFiltersModel();
            model.Finances = finances;
            var filters = filterBuilder.GetControls(model);
            return filters;
        }

        public async Task<DashboardLayout> GetFinances(ToolCode toolCode, List<FormControlValue> filters)
        {
            var capitals = await m_FinancesContextService.GetCapitals();
            foreach (var capital in capitals)
                await capital.InitializeOptionsAsync(m_ObjectContextService);//todo optimize!
            var dashboardTileCode = toolCode.GetDashboardTileCode();
            var dashboardEditorModel = await m_LayoutContextService.GetLayout<DashboardEditorModel>((int)dashboardTileCode);
            var dashboardBuilder = new CapitalDashboardBuilder(dashboardEditorModel);
            return dashboardBuilder.GetDashboardLayout(capitals);
        }

        public async Task<Grid> GetFinancesGrid(ToolCode toolCode, List<FormControlValue> filters)
        {
            var objList = new List<int>();
            DateTime? from = null;
            DateTime? to = null;
            var filtersData = filters.Select(x =>
            {
                x.ControlId.TryParseControlId(out var tileItemCode, out _);
                return (tileItemCode, x);
            }).GroupBy(x => x.tileItemCode, x => x.x.Value);
            foreach (var filter in filtersData)
            {
                if (filter.Key == TileItemCode.Object)
                {
                    foreach (var value in filters)
                    {
                        if (value.Value.TryParse<int>(out var id))
                            objList.Add(id);
                        else if (value.Value.TryParse<List<int>>(out var ids))
                            objList.AddRange(ids);
                    }
                }
            }
            var capitals = await m_FinancesContextService.GetCapitals(objList, from, to);
            foreach (var capital in capitals)
                await capital.InitializeOptionsAsync(m_ObjectContextService);//todo optimize!
            var gridTileCode = toolCode.GetGridTileCode();
            var filterLayout = await m_LayoutContextService.GetLayout<GridEditorModel>((int)gridTileCode);

            var gridBuilder = new FinancesGridBuilder(filterLayout.GridEntity.Layout);
            return gridBuilder.GetLayout(capitals);
        }
    }
}
