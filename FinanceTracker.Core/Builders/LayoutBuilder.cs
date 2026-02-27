using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.ControlDataSettings;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.DBContext;
using FinanceTracker.Data.DBModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Builders
{
    public abstract class LayoutBuilder<TData> : ILayoutBuilder<TData> where TData : class
    {
        protected readonly FinanceTrackerContext m_FinanceTrackerContext;

        public LayoutBuilder(FinanceTrackerContext financeTrackerContext)
        {
            m_FinanceTrackerContext = financeTrackerContext;
        }

        public virtual async Task<FormModel> GetFormLayout(TileCode tileCode, TData data)
        {
            var layoutData = await GetFormData(tileCode);

            var formModel = new FormModel();
            foreach (var controlData in layoutData.FormControls)
            {
                var control = new FormControl(controlData);
                control.Settings = GetControlSettings(controlData, control);
                control.Value = GetControlValue(controlData, control, data) ?? GetDefaultValue(controlData, control);
                formModel.Controls.Add(control);
            }
            return formModel;
        }

        public async Task<FormUpdateModel> UpdateFormLayout(TileCode tileCode, TData data, FormValueModel formValueModel)
        {
            var layoutData = await GetFormData(tileCode);
            var formModel = new FormUpdateModel();

            for (int i = layoutData.FormControls.Count - 1; i >= 0; i--)
            {
                var controlData = layoutData.FormControls[i];
                var control = new FormControl(controlData);
                control.Settings = GetControlSettings(controlData, control);

                if (!formValueModel.FormControlValues.TryGetValue(x => x.ControlId == controlData.Id, out var controlValue))
                    continue;

                UpdateData(data, controlData, controlValue);

                control.Value = GetControlValue(controlData, control, data);
                formModel.Controls.Add(control);
            }

            return formModel;
        }


        public abstract Task<OperationResult> SaveForm(TileCode tileCode, TData data, FormValueModel formValueModel);
        protected async Task<int> SaveLayout(Layout layout)
        {
            if (layout.Id == 0)
                await m_FinanceTrackerContext.Layouts.AddAsync(layout);
            else
                m_FinanceTrackerContext.Layouts.Update(layout);

            return await m_FinanceTrackerContext.SaveChangesAsync();
        }

        protected virtual async Task<FormLayoutData> GetFormData(TileCode tileCode)
        {
            var layoutEntity = await m_FinanceTrackerContext.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
            if (layoutEntity == null || string.IsNullOrEmpty(layoutEntity.LayoutJson))
                throw new Exception($"Failed to find layout, tile code = {tileCode}");
            var layout = JsonConvert.DeserializeObject<FormLayoutData>(layoutEntity.LayoutJson) ?? new();
            return layout;
        }


        protected ControlSettings GetControlSettings(FormControlData controlData, FormControl control)
        {
            switch (controlData.Type)
            {
                case ControlType.Input:
                    return new InputControlSettings()
                    {
                        Editable = IsEditable(controlData)
                    };
                case ControlType.Combo:
                    return new ComboControlSettings()
                    {
                        Editable = IsEditable(controlData),
                        AllowMultiselect = IsAllowMultiSelect(controlData),
                        Items = GetControlItems(controlData, control),
                    };
                case ControlType.DateTime:
                    return new DateTimeControlSettings()
                    {
                        Editable = IsEditable(controlData)
                    };
                case ControlType.Between:
                    return new BetweenControlSettings()
                    {
                        Editable = IsEditable(controlData)
                    };
                default:
                    return new ControlSettings()
                    {
                        Editable = IsEditable(controlData)
                    };
            }
        }

        protected abstract List<Item> GetControlItems(FormControlData controlData, FormControl control);
        protected abstract object? GetControlValue(FormControlData controlData, FormControl control, TData data);
        protected abstract void UpdateData(TData data, FormControlData controlData, FormControlValue controlValue);

        protected virtual object? GetDefaultValue(FormControlData controlData, FormControl control) => controlData.Type switch
        {
            ControlType.Input => null,
            ControlType.Combo when control.Settings is ComboControlSettings comboControl => comboControl.Items.FirstOrDefault(),
            ControlType.DateTime => throw new NotImplementedException(),
            ControlType.Between => throw new NotImplementedException(),
            _ => null,
        };

        private bool IsEditable(FormControlData controlData) => controlData.ControlDataSettings.ControlStates.Contains(ControlState.Editable);
        private bool IsAllowMultiSelect(FormControlData controlData) => controlData.ControlDataSettings.ControlStates.Contains(ControlState.AllowMultiselect);
    }
}
