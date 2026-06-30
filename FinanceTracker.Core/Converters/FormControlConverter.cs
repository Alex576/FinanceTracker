using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace FinanceTracker.Core.Converters
{
    public class FormControlConverter : DefaultConverter<FormControl>
    {
        public override FormControl ReadJson(JsonReader reader, Type objectType, FormControl? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObj = JObject.Load(reader);
            var formControl = new FormControl();
            formControl.Id = GetValue<string>(nameof(FormControl.Id), jsonObj);
            formControl.Name = GetValue<string>(nameof(FormControl.Name), jsonObj);
            formControl.Type = GetValue<ControlType>(nameof(FormControl.Type), jsonObj);
            formControl.TileItemCode = GetValue<TileItemCode>(nameof(FormControl.TileItemCode), jsonObj);
            formControl.Value = GetValue<JToken?>(nameof(FormControl.Value), jsonObj);

            formControl.Settings = formControl.Type switch
            {
                ControlType.Input => GetValue<InputControlSettings>(nameof(FormControl.Settings), jsonObj),
                ControlType.Combo => GetValue<ComboControlSettings>(nameof(FormControl.Settings), jsonObj),
                ControlType.DateTime => GetValue<DateTimeControlSettings>(nameof(FormControl.Settings), jsonObj),
                ControlType.Between => GetValue<BetweenControlSettings>(nameof(FormControl.Settings), jsonObj),
                _ => throw new NotImplementedException(),
            };

            return formControl;
        }
    }
}
