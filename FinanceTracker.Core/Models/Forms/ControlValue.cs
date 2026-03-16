using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Models.Forms
{
    public class ControlValue
    {
        public string ControlId { get; set; }
        public JToken? Value { get; set; }
    }
}