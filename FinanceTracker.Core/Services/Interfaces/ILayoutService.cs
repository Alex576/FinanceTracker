using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.OperationResult;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface ILayoutService
    {
        Task<LayoutEditor> GetLayoutEditor(Models.ToolCode toolCode);
        Task<LayoutManagementModel> GetLayoutManagement();
        Task<OperationResult> RemoveElement(TileCode tileCode, string itemId, EditorType type);
    }
}
