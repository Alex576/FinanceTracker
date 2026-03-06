using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.OperationResult;

namespace FinanceTracker.Core.Builders
{
    public interface ILayoutBuilder<TData> where TData : class
    {
        Task<FormModel> GetFormLayout(TileCode tileCode, TData data);
        Task<FormUpdateModel> UpdateFormLayout(TileCode tileCode, TData data, FormValueModel formValueModel);
        Task<OperationResult> SaveForm(TileCode tileCode, TData data, FormValueModel formValueModel);
    }
}
