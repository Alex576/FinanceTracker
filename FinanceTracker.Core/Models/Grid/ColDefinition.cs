namespace FinanceTracker.Core.Models.Grid
{
    public class ColDefinition
    {
        public required string Field { get; set; }
        public int ColumnId { get; set; }
        public int? Width { get; set; }
        public bool Editable { get; set; }
        public bool Filter { get; set; }
        public PinPosition Pinned { get; set; }
        public bool LockPinned { get; set; }
        public bool AutoHeight { get; set; }
        public bool WrapText { get; set; }
        public bool Sortable { get; set; }
        public int? MaxWidth { get; set; }
        public bool Resizable { get; set; }
    }
}
