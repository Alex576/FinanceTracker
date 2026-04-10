using FinanceTracker.Core.Cache;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Finances;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using MasterData.Data.Storages;

namespace FinanceTracker.Core.Builders.Filter
{
    public class CapitalsFilterBuilder : FilterBuilder<CapitalFiltersModel>
    {
        private ObjectStorage m_ObjectStorage;

        public CapitalsFilterBuilder(List<FormControlData> controlDatas, ICache cache) : base(controlDatas)
        {
            m_ObjectStorage = cache.GetObjectStorage();//todo remove cache????
        }

        protected override List<Item> GetControlItems(FormControlData controlData, FormControl control, CapitalFiltersModel data)
        {
            return controlData.TileItemCode switch
            {
                TileItemCode.Object => m_ObjectStorage.GetByClass(controlData.ControlMasterData.ClassCodes).Select(x => new Item() { Id = x.Id, Name = x.FullName }).ToList(),
                _ => []
            };
        }

        protected override object? GetControlValue(FormControlData controlData, CapitalFiltersModel data)
        {
            return controlData.TileItemCode switch
            {
                _ => null
            };
        }
    }
}
