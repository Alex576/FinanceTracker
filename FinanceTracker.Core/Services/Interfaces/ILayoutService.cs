using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.FullScreenModels;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.OperationResult;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface ILayoutService
    {
        Task<FullScreenFormEditorModel> GetForm(TileCode tileCode, List<ControlPreviewModel>? controls, FormValueModel? formValueModel);
        Task<LayoutEditor> GetLayoutEditor(Models.ToolCode toolCode);
        Task<LayoutManagementModel> GetLayoutManagement();
        Task<OperationResult> RemoveElement(TileCode tileCode, string itemId, EditorType type);
    }
}
