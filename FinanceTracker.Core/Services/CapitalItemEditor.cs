using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Services
{
    public class CapitalItemEditor : ICapitalItemEditor
    {
        public Task<FormModel> GetForm(TileCode tileCode, int capitalId)
        {
            throw new NotImplementedException();
        }

        public Task<FormModel> UpdateForm(TileCode tileCode, int capitalId, FormValueModel value)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultData<DashboardItem>> SaveForm(TileCode tileCode, int capitalId, FormValueModel value)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> RemoveCapital(int capitalId)
        {
            throw new NotImplementedException();
        }
    }
}
