using FinanceTracker.Core.Converters;
using FinanceTracker.Core.Models.Controls;
using Newtonsoft.Json;

namespace FinanceTracker.Core.Models.FullScreenModels
{
    [JsonConverter(typeof(FormComponentConverter))]
    public class FormComponent : FormControl
    {
        public string ControlGroup { get; set; }
    }
}
