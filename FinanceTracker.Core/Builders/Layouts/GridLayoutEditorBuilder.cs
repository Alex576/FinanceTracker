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
    public class GridLayoutEditorBuilder : LayoutEditorBuilder
    {
        public GridLayoutEditorBuilder(TileContextService financeTrackerContext) : base(financeTrackerContext)
        {
        }


        protected override List<FormControlData> GetEditorControls()
        {
            var baseControls = base.GetEditorControls();
            baseControls.Add(GetControl("Column Data Type", TileItemCode.ColumnDataType, ControlType.Combo, [ControlState.Editable, ControlState.Required]));
            return baseControls;
        }
    }
}
