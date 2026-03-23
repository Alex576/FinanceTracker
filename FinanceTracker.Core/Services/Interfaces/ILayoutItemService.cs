using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.OperationResult;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface ILayoutItemService
    {
        Task<OperationResult> RemoveItem(TileCode tileCode, string controlId, EditorType type);
        Task<FormModel> GetForm(TileCode tileCode, string? itemId, EditorType type);
        Task<OperationResultData<LayoutEditor>> SaveForm(SaveFormModel model);
        Task<FormModel> UpdateForm(FormValueModel model);
    }
}