using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Utils;

namespace FinanceTracker.Core.Models.LayoutEditor
{
    public class ControlDependence
    {
        public List<DependenceCriteria> Criteria { get; set; } = new();

        public void Add(DependenceCriteria criteria) => Criteria.Add(criteria);
    }

    public class DependenceCriteria
    {
        public TileItemCode TargetTileItemCode { get; set; }
        public DependencyType Type { get; set; }
        public object Value { get; set; }
    }

    public enum DependencyType
    {
        State = 1,
        Value = 2,
    }
}