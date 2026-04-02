using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;

namespace FinanceTracker.Models
{
    public class CapitalFormModel
    {
        public TileCode TileCode { get; set; }
        public int CapitalId { get; set; }
        public FormValueModel? Value { get; set; }

    }
}
