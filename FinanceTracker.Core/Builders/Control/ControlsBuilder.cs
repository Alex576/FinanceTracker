using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.ControlSettingModels;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Utils;
using Newtonsoft.Json.Linq;

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
                control.Settings = GetControlSettings(controlData, control, data);
                control.Value = GetControlValue(controlData, control, data);
                controls.Add(control);
            }
            return controls;
        }

        protected virtual List<Item> GetControlItems(FormControlData controlData, FormControl control, TData data)
        {
            return controlData.TileItemCode switch
            {
                TileItemCode.Item => EnumHelper.GetEnums<TileItemCode>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList(),
                TileItemCode.Class => EnumHelper.GetEnums<ClassCode>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList(),
                TileItemCode.DataType => EnumHelper.GetEnums<DataType>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList(),
                TileItemCode.Type => EnumHelper.GetEnums<ControlType>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList(),
                TileItemCode.State => EnumHelper.GetEnums<ControlState>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList(),
                _ => [],
            };
        }

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
            settings.Required = IsRequired(controlData);
            return settings;
        }
        protected JToken? GetControlValue(FormControlData controlData, FormControl control, TData data)
        {
            var value = GetControlValue(controlData, data);
            if (value == null)
                return GetDefaultValue(controlData, control);
            if (control.Settings is ComboControlSettings controlSettings)
                value = RestrictValueByItems(value, controlSettings);
            if (value == null)
                return GetDefaultValue(controlData, control);


            return JToken.FromObject(value);

            JToken? GetDefaultValue(FormControlData controlData, FormControl control)
            {
                return IsAutoSelectFirstValueIfEmpty(controlData) ? GetFirstValue(control) ?? this.GetDefaultValue(control) : this.GetDefaultValue(control);
            }
        }

        private JToken? GetFirstValue(FormControl control) => control.Type switch
        {
            ControlType.Combo when control.Settings is ComboControlSettings controlSettings && controlSettings.Items.Count > 0 =>
                controlSettings.AllowMultiselect ?
                    JToken.FromObject(new List<int>() { controlSettings.Items.First().Id }) :
                    controlSettings.Items.First().Id,
            _ => null,
        };

        private object? RestrictValueByItems(object value, ComboControlSettings controlSettings)
        {
            if (value is List<int> values)
                return values.Intersect(controlSettings.Items.Select(x => x.Id)).ToList();
            if (value is int intValue && controlSettings.Items.Any(x => x.Id == intValue))
                return intValue;
            if (value is Enum enumValue)
                return Convert.ToInt32(enumValue);
            return null;
        }

        protected abstract object? GetControlValue(FormControlData controlData, TData data);

        protected virtual JToken? GetDefaultValue(FormControl control) => control.Type switch
        {
            ControlType.Input => null,
            ControlType.Combo when control.Settings is ComboControlSettings controlSettings => controlSettings.AllowMultiselect ? JToken.FromObject(new List<int>()) : null,
            ControlType.DateTime => throw new NotImplementedException(),
            ControlType.Between => throw new NotImplementedException(),
            _ => null,
        };

        protected JToken? GetComboDefaultValue(ComboControlSettings settings)
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
        private bool IsRequired(FormControlData controlData) => controlData.ControlStates.Contains(ControlState.Required);
        private bool IsAutoSelectFirstValueIfEmpty(FormControlData controlData) => controlData.ControlStates.Contains(ControlState.AutoSelectFirstValueIfEmpty);
    }
}
