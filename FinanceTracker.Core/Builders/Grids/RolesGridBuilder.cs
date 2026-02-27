using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Builders.Grids
{
    public class RolesGridBuilder : GridBuilder<Role>
    {

        public override Grid GetLayout(List<Role> data)
        {
            return base.GetLayout(data);
        }

        protected override List<ColDefinition> GetColumns(Type type)
        {
            return type.GetProperties().Select((x, index) => new ColDefinition()
            {
                Field = x.Name.ToCamelCase(),
                ColumnId = index,
                Sortable = true,
                Resizable = true,
                Filter = true,
            }).ToList();
        }

        protected override List<List<object>> GetRows(List<Role> data)
        {
            return data.Select((x) => new List<object> { x.Id, x.Name }).ToList();
        }
    }
}
