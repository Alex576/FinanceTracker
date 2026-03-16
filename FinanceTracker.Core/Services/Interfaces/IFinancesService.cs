using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface IFinancesService
    {
        Task<List<FormControl>> GetFilters(ToolCode toolCode);

    }
}