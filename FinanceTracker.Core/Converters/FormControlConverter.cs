using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Converters
{
    public class FormControlConverter : JsonConverter<FormControl>
    {
        public override FormControl? ReadJson(JsonReader reader, Type objectType, FormControl? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObj = JObject.Load(reader);
            var formControl = new FormControl();
            formControl.Id = GetValue<string>(nameof(FormControl.Id), jsonObj);
            formControl.Name = GetValue<string>(nameof(FormControl.Name), jsonObj);
            formControl.Type = GetValue<ControlType>(nameof(FormControl.Type), jsonObj);
            formControl.TileItemCode = GetValue<TileItemCode>(nameof(FormControl.TileItemCode), jsonObj);
            formControl.Value = GetValue<object>(nameof(FormControl.Value), jsonObj);

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

        private T? GetValue<T>(string fieldName, JObject jsonObj)
        {
            return jsonObj.TryGetValue(fieldName, StringComparison.Ordinal, out var value) ? value.ToObject<T>() : default;
        }

        public override void WriteJson(JsonWriter writer, FormControl? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            writer.WriteStartObject();

            var properties = value.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var prop in properties)
            {
                if (prop.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any())
                    continue;
                var propName = serializer.ContractResolver is DefaultContractResolver resolver ? resolver.GetResolvedPropertyName(prop.Name) : prop.Name;
                writer.WritePropertyName(propName);
                serializer.Serialize(writer, prop.GetValue(value));
            }
            writer.WriteEndObject();
        }
    }
}
