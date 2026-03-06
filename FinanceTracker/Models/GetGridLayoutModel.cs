using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;

namespace FinanceTracker.Models
{
    public class GetGridLayoutModel
    {
        public ToolCode ToolCode { get; set; }
        public List<FormControl> Filters { get; set; } = new();
    }
}