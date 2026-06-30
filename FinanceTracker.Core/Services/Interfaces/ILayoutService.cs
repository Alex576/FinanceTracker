using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.FullScreenModels;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.OperationResult;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface ILayoutService
    {
        Task<FullScreenFormEditorModel> GetForm(TileCode tileCode, FormValueModel formValueModel);
        Task<LayoutEditor> GetLayoutEditor(Models.ToolCode toolCode);
        Task<LayoutManagementModel> GetLayoutManagement();
        Task<OperationResult> RemoveElement(TileCode tileCode, string itemId, EditorType type);
        Task<FullScreenUpdateModel> GetOptionsForm(TileCode tileCode, string selectedControl, FormValueModel? formValueModel, List<FormComponent> controls);
        Task<FullScreenUpdateModel> UpdateOptionsForm(TileCode tileCode, string selectedControl, FormValueModel formValueModel, List<FormComponent> controls);
    }
}
