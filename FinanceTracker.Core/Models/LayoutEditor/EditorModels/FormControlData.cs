using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.ControlSettingModels;

namespace FinanceTracker.Core.Models.LayoutEditor.EditorModels
{
    /// <summary>
    /// Used in layout editor forms
    /// </summary>
    public class FormControlData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ControlType Type { get; set; }

        public TileItemCode TileItemCode { get; set; }
        public List<ControlState> ControlStates { get; set; } = new();
        public ControlDependence? Dependence { get; set; }

        public ControlMasterData ControlMasterData { get; set; } = new();
    }
}
