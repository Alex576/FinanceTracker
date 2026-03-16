using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.LayoutEditor
{
    public class GridEditorModel//todo delete?
    {
        public TileCode TileCode { get; set; }
        public GridEditorLayout GridEditorLayout { get; set; } = new();
    }

    public class GridEditorLayout
    {
        public List<GridEditorColumn> Columns { get; set; } = new();
    }

    public class GridEditorColumn
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public TileItemCode TileItemCode { get; set; }
    }
}
