using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Builders.Grids
{
    public class GridEditorBuilder : GridBuilder<ColumnEntity>
    {
        public GridEditorBuilder(GridEntityLayout gridLayout) : base(gridLayout)
        {
        }

        protected override JToken? GetCellData(ColumnEntity col, ColumnEntity data)
        {
            object? value = col.TileItemCode switch
            {
                TileItemCode.Id => throw new NotImplementedException(),
                TileItemCode.Object => throw new NotImplementedException(),
                TileItemCode.Role => throw new NotImplementedException(),
                TileItemCode.UserName => throw new NotImplementedException(),
                TileItemCode.Fact => throw new NotImplementedException(),
                TileItemCode.Name => data.Name,
                TileItemCode.Type => throw new NotImplementedException(),
                TileItemCode.State => throw new NotImplementedException(),
                TileItemCode.Tool => throw new NotImplementedException(),
                TileItemCode.Tile => throw new NotImplementedException(),
                TileItemCode.Item => data.TileItemCode.ToString(),
                TileItemCode.Class => throw new NotImplementedException(),
                TileItemCode.DataType => throw new NotImplementedException(),
                TileItemCode.ColumnDataType => data.ColumnDataType.ToString(),
                _ => throw new NotImplementedException(),
            };
            if (value == null)
                return null;
            return JToken.FromObject(value);
        }

        protected override List<RowAction> GetRowActions(ColumnEntity data)
        {
            return [RowAction.Edit, RowAction.Remove];
        }

        protected override RowTag GetRowTag(ColumnEntity data)
        {
            var tag = new RowTag();
            tag.Id = ItemCodeHelper.GetItemCode(data);
            return tag;
        }
    }
}
