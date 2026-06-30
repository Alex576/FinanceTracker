using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.ControlSettingModels;
using Newtonsoft.Json;

namespace FinanceTracker.Core.Models.LayoutEditor.EditorModels
{
    /// <summary>
    /// Used in layout editor forms, stored in db
    /// </summary>
    public class FormControlData
    {
        //public string Id { get; set; }
        public string Name { get; set; }
        public ControlType Type { get; set; }

        [JsonIgnore]
        public int Order { get; set; }

        public TileItemCode TileItemCode { get; set; }
        public List<ControlState> ControlStates { get; set; } = new();
        public ControlDependence? Dependence { get; set; }

        public ControlMasterData ControlMasterData { get; set; } = new();

        [JsonConstructor]
        public FormControlData() { }
    }
}
