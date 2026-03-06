using FinanceTracker.Core.Models.Grid;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Grid> GetGridLayout();
    }
}