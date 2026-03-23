using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Models.LayoutEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Builders.Grids
{
    public class GridLayoutBuilder
    {
        public GridEntityLayout GetGridEditorLayout()
        {
            var index = 0;
            var gridEntityLayout = new GridEntityLayout();
            gridEntityLayout.Columns.Add(GetColumn(index++, TileItemCode.Name, "Name", ColumnDataType.String));
            gridEntityLayout.Columns.Add(GetColumn(index++, TileItemCode.Item, "Item", ColumnDataType.String));
            gridEntityLayout.Columns.Add(GetColumn(index++, TileItemCode.ColumnDataType, "Column Data Type", ColumnDataType.String));
            gridEntityLayout.Columns.Add(GetColumn(index++, TileItemCode.ColumnActions, "", ColumnDataType.Actions, PinPosition.Right, true));

            return gridEntityLayout;
        }

        private ColumnEntity GetColumn(int index, TileItemCode tileItemCode, string name, ColumnDataType type, PinPosition pin = PinPosition.None, bool lockPin = false)
        {
            return new ColumnEntity()
            {
                TileItemCode = tileItemCode,
                Name = name,
                ColumnDataType = type,
                Pin = pin,
                LockPin = lockPin,
            };
        }
    }
}
