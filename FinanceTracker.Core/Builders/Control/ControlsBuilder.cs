using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.ControlSettingModels;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Builders.Control
{
    public abstract class ControlsBuilder<TData> where TData : class
    {
        private readonly List<FormControlData> m_ControlDatas;

        public ControlsBuilder(List<FormControlData> controlDatas)
        {
            m_ControlDatas = controlDatas;
        }

        public List<FormControl> GetControls(TData data)
        {
            var controls = new List<FormControl>();
            foreach (var controlData in m_ControlDatas)
            {
                var control = new FormControl(controlData);
                control.Id = ItemCodeHelper.GetItemCode(controlData);
                control.Settings = GetControlSettings(controlData, control, data);
                control.Value = GetControlValue(controlData, control, data);
                controls.Add(control);
            }
            return controls;
        }

        protected abstract List<Item> GetControlItems(FormControlData controlData, FormControl control, TData data);

        protected ControlSettings GetControlSettings(FormControlData controlData, FormControl control, TData data)
        {
            ControlSettings settings = controlData.Type switch
            {
                ControlType.Input => new InputControlSettings() { },
                ControlType.Combo => new ComboControlSettings()
                {
                    AllowMultiselect = IsAllowMultiSelect(controlData),
                    Items = GetControlItems(controlData, control, data),
                },
                ControlType.DateTime => new DateTimeControlSettings() { },
                ControlType.Between => new BetweenControlSettings() { },
                _ => new ControlSettings() { },
            };
            settings.Editable = IsEditable(controlData);
            settings.Hidden = IsHidden(controlData);
            return settings;
        }
        protected JToken? GetControlValue(FormControlData controlData, FormControl control, TData data)
        {
            return GetControlValue(controlData, data) ?? GetDefaultValue(control); ;
        }

        protected abstract JToken? GetControlValue(FormControlData controlData, TData data);

        protected virtual JToken? GetDefaultValue(FormControl control) => control.Type switch
        {
            ControlType.Input => null,
            ControlType.Combo when control.Settings is ComboControlSettings controlSettings => GetComboDefaultValue(controlSettings),
            ControlType.DateTime => throw new NotImplementedException(),
            ControlType.Between => throw new NotImplementedException(),
            _ => null,
        };

        private JToken? GetComboDefaultValue(ComboControlSettings settings)
        {
            if (settings.Items == null || settings.Items.Count == 0)
                return null;

            if (settings.AllowMultiselect)
                return JToken.FromObject(new List<int> { settings.Items.First().Id });
            return settings.Items.First().Id;
        }

        private bool IsAllowMultiSelect(FormControlData controlData) => controlData.ControlStates.Contains(ControlState.AllowMultiselect);
        private bool IsEditable(FormControlData controlData) => controlData.ControlStates.Contains(ControlState.Editable);
        private bool IsHidden(FormControlData controlData) => controlData.ControlStates.Contains(ControlState.Hidden);
    }
}
