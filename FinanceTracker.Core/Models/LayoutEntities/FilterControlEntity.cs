using FinanceTracker.Core.Models.Controls;

namespace FinanceTracker.Core.Models.LayoutEntities
{
    public class FilterControlEntity : ControlEntityBase<ComboControlSettings>
    {
        public int? ObjCode { get; set; }
        public string? FactName { get; set; }
    }
}
