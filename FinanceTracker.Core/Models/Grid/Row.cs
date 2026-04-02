using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Models.Grid
{
    public class Row
    {
        public List<JToken?> Data { get; set; } = new();
        public List<RowAction> Actions { get; set; } = new();
        public RowTag Tag { get; set; } = new();
    }
}
