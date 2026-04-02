namespace MasterData.Data.Models
{
    public class FinanceOptionsDataExt
    {
        public List<ObjectModel> Objects { get; set; } = [];
        public List<AttributeData> Attributes { get; set; } = [];
        public bool ReadOnly { get; set; }
    }
}