using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.FullScreenModels;

namespace FinanceTracker.Models
{
    public class FullScreenFormModel
    {
        public FormValueModel? FormValueModel { get; set; }
        public TileCode TileCode{ get; set; }
        public List<ControlPreviewModel>? Controls { get; set; }
    }
}
