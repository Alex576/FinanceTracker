using FinanceTracker.Core.Models.Grid;

namespace FinanceTracker.Core.Models.LayoutEditor.GridEditor
{
    public class ColumnPropertyEntity : IColDefinitionProperties
    {
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