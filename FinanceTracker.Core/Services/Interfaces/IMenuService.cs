using FinanceTracker.Core.Models;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuItem>> GetMenuItems();
    }
}