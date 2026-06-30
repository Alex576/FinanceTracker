using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.ControlSettingModels;
using FinanceTracker.Core.Models.FullScreenModels;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Data.Services;

namespace FinanceTracker.Core.Builders.Layouts
{
    public class FormLayoutEditorBuilder : LayoutEditorBuilder
    {
        public FormLayoutEditorBuilder(TileContextService financeTrackerContext) : base(financeTrackerContext)
        {
        }

        protected override List<FormControlData> GetEditorControls()
        {
            var controls = new List<FormControlData>();

            controls.Add(GetControl("Name", TileItemCode.Name, ControlType.Input, [ControlState.Editable, ControlState.Required]));
            var itemControl = GetControl("Item", TileItemCode.Item, ControlType.Combo, [ControlState.Editable, ControlState.Required]);
            controls.Add(itemControl);
            controls.Add(GetControl("State", TileItemCode.State, ControlType.Combo, [ControlState.Editable, ControlState.AllowMultiselect]));
            controls.Add(GetControl("Type", TileItemCode.Type, ControlType.Combo, [ControlState.Editable, ControlState.Required]));

            var classControlDependsOnItem = GetControlDependence(TileItemCode.Item, DependencyType.Value, TileItemCode.Object);
            //if (m_FormValueModel.TryGetControlValue<int>(x => x.ControlId == itemControl.Id, out var controlValue) && controlValue == (int)TileItemCode.Object)
            controls.Add(GetControl("Class", TileItemCode.Class, ControlType.Combo, [ControlState.Hidden, ControlState.Editable, ControlState.AllowMultiselect], classControlDependsOnItem));
            return controls;

        }
    }
}
