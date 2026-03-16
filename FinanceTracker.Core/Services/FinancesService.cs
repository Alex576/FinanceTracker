using FinanceTracker.Core.Builders.Filter;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Finances;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.DBContext;
using FinanceTracker.Data.Services;
using MasterData.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Core.Services
{
    public class FinancesService : IFinancesService
    {
        private readonly LayoutContextService m_LayoutContextService;
        private readonly FinancesContextService m_FinancesContextService;

        public FinancesService(LayoutContextService layoutContextService, FinancesContextService financesContextService)
        {
            m_LayoutContextService = layoutContextService;
            m_FinancesContextService = financesContextService;
        }

        public async Task<List<FormControl>> GetFilters(ToolCode toolCode)
        {
            var tileCode = toolCode.GetFilterTileCode();
            var filterLayout = await m_LayoutContextService.GetLayout<LayoutEditorModel>((int)tileCode);
            if (filterLayout == null)
                return new List<FormControl>();

            var filterBuilder = new FinancesFilterBuilder(filterLayout.Layout.FormControls);
            var finances = await m_FinancesContextService.GetAllFinances(0);
            var model = new FinanceFiltersModel();
            model.Finances = finances;
            var filters = filterBuilder.GetControls(model);
            return filters;
        }
    }
}
