using FinanceTracker.Core.Models.FullScreenModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Converters
{
    internal class FormComponentConverter : DefaultConverter<FormComponent>
    {
        public override FormComponent ReadJson(JsonReader reader, Type objectType, FormComponent? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var formControlConverter = new FormControlConverter();
            FormComponent formComponent = (FormComponent)formControlConverter.ReadJson(reader, objectType, existingValue, hasExistingValue, serializer);
            var jsonObj = JObject.Load(reader);
            formComponent.ControlGroup = GetValue<string>(nameof(FormComponent.ControlGroup), jsonObj);
            return formComponent;
        }
    }
}
