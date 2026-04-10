using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.ControlSettingModels;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Data.Services;

namespace FinanceTracker.Core.Builders.Layouts
{
    public class GridLayoutEditorBuilder : LayoutEditorBuilder
    {
        public GridLayoutEditorBuilder(TileContextService financeTrackerContext) : base(financeTrackerContext)
        {
        }


        protected override List<FormControlData> GetEditorControls()
        {
            var controls = base.GetEditorControls();
            controls.Add(GetControl("Name", TileItemCode.Name, ControlType.Input, [ControlState.Editable, ControlState.Required]));
            controls.Add(GetControl("Item", TileItemCode.Item, ControlType.Combo, [ControlState.Editable, ControlState.Required]));
            controls.Add(GetControl("Column Data Type", TileItemCode.ColumnDataType, ControlType.Combo, [ControlState.Editable, ControlState.Required]));
            controls.Add(GetControl("State", TileItemCode.State, ControlType.Combo, [ControlState.Editable, ControlState.AllowMultiselect]));
            controls.Add(GetControl("Class", TileItemCode.Class, ControlType.Combo, [ControlState.Hidden, ControlState.Editable, ControlState.AllowMultiselect]));

            return controls;
        }
    }
}
