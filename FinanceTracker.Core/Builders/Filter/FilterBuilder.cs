using FinanceTracker.Core.Builders.Control;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;

namespace FinanceTracker.Core.Builders.Filter
{
    public abstract class FilterBuilder<TData> : ControlsBuilder<TData> where TData : class
    {
        protected FilterBuilder(List<FormControlData> controlDatas) : base(controlDatas)
        {
        }
    }
}
