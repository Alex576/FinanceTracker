using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.LayoutEditor.FormEditorModels
{
    /// <summary>
    /// Stored in db
    /// </summary>
    public class FormEditorModel : ItemEditorModelBase
    {
        public List<FormControlData> Controls { get; set; } = [];
        public FormEditorModel(TileCode tileCode) : base(tileCode) { }
    }
}
