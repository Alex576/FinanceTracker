using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;

namespace FinanceTracker.Controllers.Api
{
    public interface IFormEditorService
    {
        Task<FormModel> GetForm(TileCode tileCode);
    }
}