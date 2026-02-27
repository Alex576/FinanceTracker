namespace FinanceTracker.Core.Models.LayoutPreviews
{
    public abstract class BasePreviewItem
    {
        public TileCode TileCode { get; set; }

        protected BasePreviewItem(TileCode tileCode)
        {
            TileCode = tileCode;
        }
    }
}
