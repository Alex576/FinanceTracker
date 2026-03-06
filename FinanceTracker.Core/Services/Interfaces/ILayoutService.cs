using FinanceTracker.Core.Models.LayoutEditor;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface ILayoutService
    {
        Task<LayoutEditor> GetLayoutEditor(Models.ToolCode toolCode);
        Task<LayoutManagementModel> GetLayoutManagement();
    }
}
