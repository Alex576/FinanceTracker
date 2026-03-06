using FinanceTracker.Core.Models.LayoutEditor;

namespace FinanceTracker.Core.Models.ControlSettingModels
{
    public class ControlMasterData
    {
        public Dictionary<AttributeCode, object> Attributes { get; set; } = new();
        public List<int> ObjCodes { get; set; } = new();
        public List<int> ClassCodes { get; set; } = new();
        public FactModel FactModel { get; set; } = new();
    }
}