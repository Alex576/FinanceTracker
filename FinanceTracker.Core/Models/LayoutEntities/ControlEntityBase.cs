using FinanceTracker.Core.Models.Controls;

namespace FinanceTracker.Core.Models.LayoutEntities
{
    public class ControlEntityBase<TSettings> where TSettings : ControlSettings
    {
        public string Name { get; set; }
        public TileItemCode TileItemCode { get; set; }
        public TSettings Settings { get; set; }

    }
}