using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Finances;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;

namespace FinanceTracker.Core.Builders.Filter
{
    public class FinancesFilterBuilder : FilterBuilder<FinanceFiltersModel>
    {
        public FinancesFilterBuilder(List<FormControlData> controlDatas) : base(controlDatas)
        {
        }

        protected override List<Item> GetControlItems(FormControlData controlData, FormControl control, FinanceFiltersModel data)
        {
            return controlData.TileItemCode switch
            {
                //TileItemCode.Object => data.Finances.Select(x=>x.Options)
                _ => []
            };
        }

        protected override object? GetControlValue(FormControlData controlData, FinanceFiltersModel data)
        {
            return controlData.TileItemCode switch
            {
                _ => null
            };
        }
    }
}
