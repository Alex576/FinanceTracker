using FinanceTracker.Core.Converters;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Models.Controls
{
    [JsonConverter(typeof(FormControlConverter))]
    public class FormControl
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ControlType Type { get; set; }
        public ControlSettings Settings { get; set; }
        public JToken? Value { get; set; }
        public TileItemCode TileItemCode { get; set; }
        public bool Updated { get; set; }


        [JsonConstructor]
        public FormControl() { }

        public FormControl(FormControlData formControlData)
        {
            Id = formControlData.Id;
            Name = formControlData.Name;
            Type = formControlData.Type;
            TileItemCode = formControlData.TileItemCode;
        }

    }
}