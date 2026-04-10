using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.OperationResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface ICapitalService
    {
        Task<FormModel> GetForm(TileCode tileCode, int capitalId);
        Task<FormModel> UpdateForm(TileCode tileCode, int capitalId, FormValueModel value);
        Task<OperationResultData<DashboardItem>> SaveForm(TileCode tileCode, int capitalId, FormValueModel value);
        Task<OperationResult> RemoveCapital(int capitalId);
        Task<List<FormControl>> GetFilters(ToolCode toolCode);
        Task<DashboardLayout> GetCapitals(ToolCode toolCode, List<FormControlValue> filters);


    }
}
