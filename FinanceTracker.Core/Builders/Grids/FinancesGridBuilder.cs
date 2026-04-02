using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Models.LayoutEntities;
using FinanceTracker.Core.Utils;
using MasterData.Data.Models;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Builders.Grids
{
    public class FinancesGridBuilder : GridBuilder<FinanceModel>
    {
        public FinancesGridBuilder(GridEntityLayout gridLayout) : base(gridLayout)
        {
        }

        protected override JToken? GetCellData(ColumnEntity col, FinanceModel data)
        {
            object? value = col.TileItemCode switch
            {
                TileItemCode.Id => data.Id,
                TileItemCode.Object => GetObject(col, data),
                TileItemCode.Fact => throw new NotImplementedException(),
                TileItemCode.Name => throw new NotImplementedException(),
                TileItemCode.Attribute => GetAttribute(col, data),
                _ => throw new NotImplementedException(),
            };

            return value == null ? null : JToken.FromObject(value);
        }

        private object? GetObject(ColumnEntity col, FinanceModel data)
        {
            var objects = data.OptionsExt.Objects.IntersectBy(col.ControlMasterData.ClassCodes, x => x.ClassCode).ToList();
            return col.ColumnDataType switch
            {
                ColumnDataType.String => string.Join(", ", objects.Select(x => x.FullName)),
                _ => throw new NotImplementedException(),
            };
        }

        private object? GetAttribute(ColumnEntity col, FinanceModel data)
        {
            if (string.IsNullOrEmpty(col.AttributeName))
                return null;
            if (data.Options.Attributes.TryGetValue(x => x.Name == col.AttributeName, out var attributeData))
            {
                switch (col.ColumnDataType)
                {
                    case ColumnDataType.Number:
                        break;
                    case ColumnDataType.String:
                        return attributeData.Value.TryParse(out string stringValue) ? stringValue : null;
                    case ColumnDataType.Float:
                        break;
                    case ColumnDataType.DateTime:
                        break;
                    case ColumnDataType.DateRange:
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            return null;

        }

        protected override List<RowAction> GetRowActions(FinanceModel data)
        {
            return data.Options.ReadOnly ? [RowAction.Show] : [RowAction.Edit];
        }

        protected override RowTag GetRowTag(FinanceModel data)
        {
            return new RowTag() { Id = data.Id.ToString() };
        }
    }
}
