using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Utils;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Builders.Grids
{
    public class RolesGridBuilder : GridBuilder<Role>
    {
        public RolesGridBuilder(GridEntityLayout gridLayout) : base(gridLayout)
        {
        }

        protected override JToken? GetCellData(ColumnEntity col, Role data)
        {
            throw new NotImplementedException();
        }
    }
}
