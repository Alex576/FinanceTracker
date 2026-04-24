using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Models.LayoutEditor.FormEditorModels;
using FinanceTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.LayoutEntities
{
    /// <summary>
    /// Used in layout editor
    /// </summary>
    public class FormLayoutEntity : LayoutEntityBase
    {
        public override TileTypeCode Type => TileTypeCode.Form;
        public List<FormEditorControlEntity> Controls { get; set; } = [];

        public FormLayoutEntity(TileCode tileCode) : base(tileCode)
        {
        }

    }
}
