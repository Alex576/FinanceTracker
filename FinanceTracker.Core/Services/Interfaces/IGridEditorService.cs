using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.OperationResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface IGridEditorService
    {
        Task<FormModel> GetForm(TileCode tileCode, string? itemId);
        Task<FormModel> UpdateForm(FormValueModel model);
        Task<OperationResultData<LayoutEditor>> SaveForm(SaveFormModel model);
    }
}
