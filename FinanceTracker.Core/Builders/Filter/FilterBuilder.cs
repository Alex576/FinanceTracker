using FinanceTracker.Core.Builders.Control;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Models.UserSettings;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Builders.Filter
{
    public abstract class FilterBuilder<TData> : ControlsBuilder<TData> where TData : class
    {
        protected FilterBuilder(List<FormControlData> controlDatas) : base(controlDatas)
        {
        }
    }
}
