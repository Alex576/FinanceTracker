namespace FinanceTracker.Core.Models.LayoutEditor.EditorModels
{
    /// <summary>
    /// used in layout editor forms
    /// </summary>
    public class FormLayoutData
    {
        public TileCode TileCode { get; set; }
        public FormControlData FormControl { get; set; } = new();
    }
}
