using FinanceTracker.Core.Builders.Forms;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Data.Services;
using MasterData.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Builders.Dashboards
{
    public class CapitalDashboardFormBuilder : FormBuilder<FinanceModel>
    {
        public CapitalDashboardFormBuilder(FinanceContextServiceBase financeContextServiceBase, LayoutEditorModel layoutModel) : base(financeContextServiceBase, layoutModel)
        {
        }

        protected override List<Item> GetControlItems(FormControlData controlData, FormControl control, FinanceModel data)
        {
            throw new NotImplementedException();
        }

        protected override object? GetControlValue(FormControlData controlData, FinanceModel data)
        {
            throw new NotImplementedException();
        }

        protected override Task<OperationResult> SaveLayout(TileCode tileCode, FinanceModel data, FormValueModel formValueModel)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateData(FinanceModel data, FormControlData controlData, FormControlValue controlValue)
        {
            throw new NotImplementedException();
        }
    }
}
