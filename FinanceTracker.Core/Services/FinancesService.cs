using FinanceTracker.Core.Builders.Filter;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Core.Services
{
    public class FinancesService : IFinancesService
    {
        private readonly FinanceTrackerContext m_FinanceTrackerContext;

        public FinancesService(FinanceTrackerContext financeTrackerContext)
        {
            m_FinanceTrackerContext = financeTrackerContext;
        }

        public async Task<List<FormControl>> GetFilters(ToolCode toolCode)
        {
            var tileCode = toolCode.GetFilterTileCode();
            var filterLayout = await m_FinanceTrackerContext.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
            if (filterLayout == null)
                return new List<FormControl>();

            var filterBuilder = new FilterBuilder();

            return null;
        }
    }
}
