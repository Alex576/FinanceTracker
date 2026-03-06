using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.OperationResult;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface IFormEditorService
    {
        Task<OperationResult> DeleteControl(TileCode tileCode, string controlId);
        Task<FormModel> GetForm(TileCode tileCode, string? itemId);
        Task<OperationResultData<LayoutEditor>> SaveForm(SaveFormModel model);
        Task<FormModel> UpdateForm(FormValueModel model);
    }
}