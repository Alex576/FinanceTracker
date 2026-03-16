using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Forms;

namespace FinanceTracker.Models
{
    public class GetGridLayoutModel
    {
        public ToolCode ToolCode { get; set; }
        public List<FormControlValue> Filters { get; set; } = new();
    }
}