namespace FinanceTracker.Core.Models.Grid
{
    public interface IColDefinitionProperties
    {
        int? Width { get; set; }
        bool Editable { get; set; }
        bool Filter { get; set; }
        PinPosition Pin { get; set; }
        bool LockPin { get; set; }
        bool AutoHeight { get; set; }
        bool WrapText { get; set; }
        bool Sortable { get; set; }
        int? MaxWidth { get; set; }
        bool Resizable { get; set; }
    }
}