using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.OperationResult;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface IGridEditorService
    {
        Task<FormModel> GetForm(TileCode tileCode, string? itemId);
        Task<FormModel> UpdateForm(TileCode tileCode, string? itemId, EditorType type, FormValueModel value);
        Task<OperationResultData<LayoutEditor>> SaveForm(TileCode tileCode, string? itemId, EditorType type, SaveFormModel value);
    }
}
