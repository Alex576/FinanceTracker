using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.Grid;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface IFinancesService
    {
        Task<List<FormControl>> GetFilters(ToolCode toolCode);
        Task<Grid> GetFinancesGrid(ToolCode toolCode, List<FormControlValue> filters);
        Task<DashboardLayout> GetFinances(ToolCode toolCode, List<FormControlValue> filters);
    }
}