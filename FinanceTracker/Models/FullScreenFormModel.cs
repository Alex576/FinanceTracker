using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.FullScreenModels;

namespace FinanceTracker.Models
{
    public class FullScreenFormModel
    {
        public FormValueModel? FormValueModel { get; set; } = new();
        public TileCode TileCode { get; set; }
        public List<FormComponent> Controls { get; set; } = [];
        public string? SelectedControl { get; set; }
    }
}
