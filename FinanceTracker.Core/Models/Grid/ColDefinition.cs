using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Models.LayoutEntities;

namespace FinanceTracker.Core.Models.Grid
{
    public class ColDefinition : IColDefinitionProperties
    {
        public ColDefinition(ColumnEntity col)
        {
            Field = col.Name;
            //ColumnId = col.ColumnId;
            ColumnDataType = col.ColumnDataType;
            Pin = col.Pin;
            LockPin = col.LockPin;
        }

        public string Field { get; set; }
        public string ColumnId { get; set; }
        public ColumnDataType ColumnDataType { get; set; }
        public int? Width { get; set; }
        public bool Editable { get; set; }
        public bool Filter { get; set; }
        public PinPosition Pin { get; set; }
        public bool LockPin { get; set; }
        public bool AutoHeight { get; set; }
        public bool WrapText { get; set; }
        public bool Sortable { get; set; }
        public int? MaxWidth { get; set; }
        public bool Resizable { get; set; }
    }
}
