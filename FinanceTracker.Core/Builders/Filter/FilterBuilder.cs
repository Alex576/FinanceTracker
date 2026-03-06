using FinanceTracker.Core.Models.Controls;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Builders.Filter
{
    public class FilterBuilder
    {
        private int _index = 0;
        //public FormControl GetFilterControl(List<Item> items, string name, ComboControlSettings? settings = null, string? controlId = null)
        //{
        //    var filterControl = new FormControl();
        //    filterControl.Id = string.IsNullOrEmpty(controlId) ? $"{_index++}" : controlId;
        //    filterControl.Name = name;
        //    filterControl.Settings = settings ?? new ComboControlSettings() { AllowMultiselect = true, Editable = true, Items = items };
        //    filterControl.Value = FillEmptyValue(items, settings);
        //    return filterControl;
        //}

        public JToken? FillEmptyValue(ControlSettings settings)
        {
            if (settings is ComboControlSettings controlSettings)
            {
                if (controlSettings.Items == null || controlSettings.Items.Count == 0)
                    return null;

                if (controlSettings.AllowMultiselect == true)
                    return JToken.FromObject(new List<int> { controlSettings.Items.First().Id });
                return controlSettings.Items.First().Id;
            }
            return null;
        }
    }
}
