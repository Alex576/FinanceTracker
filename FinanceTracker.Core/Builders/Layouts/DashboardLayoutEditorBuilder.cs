using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.ControlSettingModels;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Builders.Layouts
{
    public class DashboardLayoutEditorBuilder : LayoutEditorBuilder
    {
        public DashboardLayoutEditorBuilder(TileContextService financeTrackerContext) : base(financeTrackerContext)
        {
        }

        protected override List<FormControlData> GetEditorControls()
        {
            var controls = base.GetEditorControls();
            controls.Add(GetControl("Name", TileItemCode.Name, ControlType.Input, [ControlState.Editable, ControlState.Required]));
            //controls.Add(GetControl("Fact", TileItemCode.Fact, ControlType.Combo, [ControlState.Editable, ControlState.Required]));
            controls.Add(GetControl("Item", TileItemCode.Item, ControlType.Combo, [ControlState.Editable, ControlState.Required]));
            //controls.Add(GetControl("Column Data Type", TileItemCode.ColumnDataType, ControlType.Combo, [ControlState.Editable, ControlState.Required]));
            //controls.Add(GetControl("State", TileItemCode.State, ControlType.Combo, [ControlState.Editable, ControlState.AllowMultiselect]));
            //controls.Add(GetControl("Class", TileItemCode.Class, ControlType.Combo, [ControlState.Hidden, ControlState.Editable, ControlState.AllowMultiselect, ControlState.Required]));

            return controls;
        }
    }
}
