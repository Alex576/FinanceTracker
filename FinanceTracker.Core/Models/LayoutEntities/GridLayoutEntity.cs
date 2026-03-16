using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.ControlSettingModels;
using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Data.Models;

namespace FinanceTracker.Core.Models.LayoutEntities
{
    public class GridLayoutEntity : LayoutEntityBase
    {
        public override TileTypeCode Type => TileTypeCode.Grid;
        public List<ColumnEntity> Columns { get; set; } = [];
        public GridLayoutEntity(TileCode tileCode) : base(tileCode)
        {
        }
    }

    public class ColumnEntity
    {
        public string Name { get; set; }
        public string ColumnId { get; set; }
        public TileItemCode TileItemCode { get; set; }
        public ColumnDataType ColumnDataType { get; set; }
        public ControlMasterData ControlMasterData { get; set; } = new();
        public List<ControlState> ControlStates { get; set; } = new();

    }
}
