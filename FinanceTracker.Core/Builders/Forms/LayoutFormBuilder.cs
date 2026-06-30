using FinanceTracker.Core.Builders.Control;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.FullScreenModels;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Builders.Forms
{
    public class LayoutFormBuilder : ControlsBuilder<FullScreenFormEditorModel>
    {
        public LayoutFormBuilder(List<FormControlData> controlDatas) : base(controlDatas)
        {
        }

        protected override object? GetControlValue(FormControlData controlData, FullScreenFormEditorModel data)
        {
            throw new NotImplementedException();
        }
    }
}
