using FinanceTracker.Core.Builders.Dashboards;
using FinanceTracker.Core.Builders.Filter;
using FinanceTracker.Core.Cache;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Core.Models.Finances;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.DashboardEditor;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.Services;
using MasterData.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Services
{
    public class CapitalService : ICapitalService
    {
        private readonly LayoutContextService m_LayoutContextService;
        private readonly FinancesContextService m_FinancesContextService;
        private readonly ObjectContextService m_ObjectContextService;
        private readonly ICache m_Cache;

        public CapitalService(LayoutContextService layoutContextService, FinancesContextService financesContextService, ObjectContextService objectContextService, ICache cache)
        {
            m_LayoutContextService = layoutContextService;
            m_FinancesContextService = financesContextService;
            m_ObjectContextService = objectContextService;
            m_Cache = cache;
        }

        public Task<FormModel> GetForm(TileCode tileCode, int capitalId)
        {
            throw new NotImplementedException();
        }

        public Task<FormModel> UpdateForm(TileCode tileCode, int capitalId, FormValueModel value)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultData<DashboardItem>> SaveForm(TileCode tileCode, int capitalId, FormValueModel value)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> RemoveCapital(int capitalId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FormControl>> GetFilters(ToolCode toolCode)
        {
            var tileCode = toolCode.GetFilterTileCode();
            var filterLayout = await m_LayoutContextService.GetLayout<LayoutEditorModel>((int)tileCode);
            if (filterLayout == null)
                return new List<FormControl>();

            var filterBuilder = new CapitalsFilterBuilder(filterLayout.FormControls, m_Cache);
            var finances = await m_FinancesContextService.GetAllFinances();
            var model = new CapitalFiltersModel();
            model.Finances = finances;
            var filters = filterBuilder.GetControls(model);
            return filters;
        }

        public async Task<DashboardLayout> GetCapitals(ToolCode toolCode, List<FormControlValue> filters)
        {
            var objectStorage = m_Cache.GetObjectStorage();
            var capitals = await m_FinancesContextService.GetCapitals();
            foreach (var capital in capitals)
                capital.InitializeOptions(objectStorage);
            var dashboardTileCode = toolCode.GetDashboardTileCode();
            var dashboardEditorModel = await m_LayoutContextService.GetLayout<DashboardEditorModel>((int)dashboardTileCode);
            var dashboardBuilder = new CapitalDashboardBuilder(dashboardEditorModel);
            return dashboardBuilder.GetDashboardLayout(capitals);
        }
    }
}
