using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Models.Forms
{
    public class FormControlValue
    {
        public string ControlId { get; set; }
        public JToken? Value { get; set; }
        public bool Updated { get; set; }
    }
}