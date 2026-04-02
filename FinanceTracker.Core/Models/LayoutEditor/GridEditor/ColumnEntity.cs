using FinanceTracker.Core.Models.ControlSettingModels;
using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Core.Models.LayoutEntities;

namespace FinanceTracker.Core.Models.LayoutEditor.GridEditor
{
    public class ColumnEntity
    {
        public string Name { get; set; }
        //public string ColumnId { get; set; }
        public TileItemCode TileItemCode { get; set; }
        public ColumnDataType ColumnDataType { get; set; }
        public ControlMasterData ControlMasterData { get; set; } = new();
        public List<ControlState> ControlStates { get; set; } = new();
        public PinPosition Pin { get; set; }
        public bool LockPin { get; set; }
        public string? AttributeName { get; set; }
    }
}
