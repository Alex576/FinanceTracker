namespace FinanceTracker.Core.Models.LayoutEditor
{
    public class ItemEditorModelBase
    {
        public TileCode TileCode { get; set; }

        public ItemEditorModelBase(TileCode tileCode)
        {
            TileCode = tileCode;
        }
    }
}