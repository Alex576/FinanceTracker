using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.LayoutEditor.FormEditorModels
{
    public class FormEditorControlEntity
    {
        public string Name { get; set; }
        public ControlType Type { get; set; }
        public TileItemCode TileItemCode { get; set; }
        public FormEditorControlEntity(FormControlData formControlData)
        {
            Name = formControlData.Name;
            Type = formControlData.Type;
            TileItemCode = formControlData.TileItemCode;
        }
    }
}
