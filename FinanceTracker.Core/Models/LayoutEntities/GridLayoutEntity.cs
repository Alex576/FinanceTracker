using FinanceTracker.Data.Models;

namespace FinanceTracker.Core.Models.LayoutEntities
{
    /// <summary>
    /// Used in layout editor
    /// </summary>
    public class GridLayoutEntity : LayoutEntityBase
    {
        public override TileTypeCode Type => TileTypeCode.Grid;
        public GridEditorEntity GridEditor { get; set; } = new();
        public GridLayoutEntity(TileCode tileCode) : base(tileCode)
        {
        }
    }
}
