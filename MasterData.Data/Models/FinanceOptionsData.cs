namespace MasterData.Data.Models
{
    public class FinanceOptionsData
    {
        public List<int> ObjCodes { get; set; } = [];
        public List<AttributeData> Attributes { get; set; } = [];
        public bool ReadOnly { get; set; }
    }
}
