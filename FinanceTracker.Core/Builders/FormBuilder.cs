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

namespace FinanceTracker.Core.Builders
{
    public abstract class FormBuilder<TData> : ILayoutBuilder<TData> where TData : class
    {
        protected readonly FinanceContextService m_FinanceContextService;
        protected readonly LayoutEditorModel m_LayoutModel;
        public FormBuilder(FinanceContextService financeContextService, LayoutEditorModel layoutModel)
        {
            m_FinanceContextService = financeContextService;
            m_LayoutModel = layoutModel;
        }

        public virtual async Task<FormModel> GetFormLayout(TileCode tileCode, TData data)
        {
            var formModel = new FormModel(tileCode);
            foreach (var controlData in m_LayoutModel.FormControls)
            {
                var control = new FormControl(controlData);
                control.Settings = GetControlSettings(controlData, control);
                control.Value = GetControlValue(controlData, control, data);
                formModel.Controls.Add(control);
            }
            formModel.Actions = GetFormActions();
            return formModel;
        }

        public async Task<FormUpdateModel> UpdateFormLayout(TileCode tileCode, TData data, FormValueModel formValueModel)
        {
            var formModel = new FormUpdateModel(tileCode);

            UpdateDataByFormValues(data, formValueModel);
            foreach (var controlData in m_LayoutModel.FormControls)
            {
                var control = new FormControl(controlData);
                control.Settings = GetControlSettings(controlData, control);
                control.Value = GetControlValue(controlData, control, data);
                //control.Updated =
                formModel.Controls.Add(control);
            }
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

        protected ControlSettings GetControlSettings(FormControlData controlData, FormControl control)
        {
            ControlSettings settings = controlData.Type switch
            {
                ControlType.Input => new InputControlSettings() { },
                ControlType.Combo => new ComboControlSettings()
                {
                    AllowMultiselect = IsAllowMultiSelect(controlData),
                    Items = GetControlItems(controlData, control),
                },
                ControlType.DateTime => new DateTimeControlSettings() { },
                ControlType.Between => new BetweenControlSettings() { },
                _ => new ControlSettings() { },
            };
            settings.Editable = IsEditable(controlData);
            settings.Hidden = IsHidden(controlData);
            return settings;
        }

        protected abstract List<Item> GetControlItems(FormControlData controlData, FormControl control);
        protected JToken? GetControlValue(FormControlData controlData, FormControl control, TData data)
        {
            return GetControlValue(controlData, data) ?? GetDefaultValue(controlData, control); ;
        }

        protected abstract JToken? GetControlValue(FormControlData controlData, TData data);
        protected abstract void UpdateData(TData data, FormControlData controlData, FormControlValue controlValue);
        protected virtual List<FormAction> GetFormActions() => [new() { Code = FormActionCode.Save }];

        protected virtual JToken? GetDefaultValue(FormControlData controlData, FormControl control) => controlData.Type switch
        {
            ControlType.Input => null,
            ControlType.Combo when control.Settings is ComboControlSettings comboControl && comboControl.Items.Count > 0 => JToken.FromObject(comboControl.Items.First().Id),
            ControlType.DateTime => throw new NotImplementedException(),
            ControlType.Between => throw new NotImplementedException(),
            _ => null,
        };

        private bool IsEditable(FormControlData controlData) => controlData.ControlStates.Contains(ControlState.Editable);
        private bool IsHidden(FormControlData controlData) => controlData.ControlStates.Contains(ControlState.Hidden);
        private bool IsAllowMultiSelect(FormControlData controlData) => controlData.ControlStates.Contains(ControlState.AllowMultiselect);
    }
}
