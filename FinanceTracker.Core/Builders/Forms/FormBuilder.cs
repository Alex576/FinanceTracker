using FinanceTracker.Core.Builders.Control;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.ControlSettingModels;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.DBContext;
using FinanceTracker.Data.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Builders.Forms
{
    public abstract class FormBuilder<TData> : ControlsBuilder<TData>, ILayoutBuilder<TData> where TData : class
    {
        protected readonly FinanceContextServiceBase m_FinanceContextServiceBase;
        protected readonly LayoutEditorModel m_LayoutModel;
        public FormBuilder(FinanceContextServiceBase financeContextServiceBase, LayoutEditorModel layoutModel) : base(layoutModel.FormControls)
        {
            m_FinanceContextServiceBase = financeContextServiceBase;
            m_LayoutModel = layoutModel;
        }

        public virtual async Task<FormModel> GetFormLayout(TileCode tileCode, TData data)
        {
            var formModel = new FormModel(tileCode);
            formModel.Controls.AddRange(GetControls(data));
            formModel.Actions = GetFormActions();
            return formModel;
        }

        public async Task<FormUpdateModel> UpdateFormLayout(TileCode tileCode, TData data, FormValueModel formValueModel)
        {
            var formModel = new FormUpdateModel(tileCode);

            UpdateDataByFormValues(data, formValueModel);
            formModel.Controls.AddRange(GetControls(data));
            formModel.Actions = GetFormActions();
            return formModel;
        }

        private void UpdateDataByFormValues(TData data, FormValueModel formValueModel)
        {
            for (int i = m_LayoutModel.FormControls.Count - 1; i >= 0; i--)
            {
                var controlData = m_LayoutModel.FormControls[i];
                if (!formValueModel.UpdatedControls.TryGetValue(x => x.ControlId == controlData.Id, out var controlValue))// || controlValue.Updated)
                    continue;

                UpdateData(data, controlData, controlValue);
            }
        }

        public async Task<OperationResult> SaveForm(TileCode tileCode, TData data, FormValueModel formValueModel)
        {
            UpdateDataByFormValues(data, formValueModel);
            return await SaveLayout(tileCode, data, formValueModel);
        }

        protected abstract Task<OperationResult> SaveLayout(TileCode tileCode, TData data, FormValueModel formValueModel);
        protected abstract void UpdateData(TData data, FormControlData controlData, FormControlValue controlValue);
        protected virtual List<FormAction> GetFormActions() => [new() { Code = FormActionCode.Save }];
    }
}
