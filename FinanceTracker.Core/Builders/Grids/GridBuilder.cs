using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.ControlSettingModels;
using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Builders.Grids
{
    public abstract class GridBuilder<T>
    {
        protected readonly GridEntityLayout m_GridLayout;

        protected List<ColumnEntity> Columns;

        public GridBuilder(GridEntityLayout gridLayout)
        {
            m_GridLayout = gridLayout;
            PrepareColumns(gridLayout.Columns);
        }

        private void PrepareColumns(List<ColumnEntity> columns)
        {
            Columns = columns.Where(c => !IsHidden(c)).ToList();
        }

        public virtual Grid GetLayout(List<T> data)
        {
            var grid = new Grid();
            var layout = new Layout();
            layout.Cols = GetColumns();
            grid.Rows = GetRows(data);
            grid.Layout = layout;

            return grid;
        }

        protected List<Row> GetRows(List<T> data)
        {
            var rows = new List<Row>(data.Count);
            foreach (var rowData in data)
                rows.Add(GetRowData(rowData));
            return rows;
        }

        protected Row GetRowData(T data)
        {
            var row = new Row();
            foreach (var col in Columns)
            {
                if (col.TileItemCode == TileItemCode.ColumnActions)
                {
                    row.Data.Add(null);
                }
                else
                {
                    row.Data.Add(GetCellData(col, data));
                }
            }
            row.Actions.AddRange(GetRowActions(data));
            row.Tag = GetRowTag(data);
            return row;
        }

        protected abstract RowTag GetRowTag(T data);
        protected abstract List<RowAction> GetRowActions(T data);
        protected abstract JToken? GetCellData(ColumnEntity col, T data);

        protected List<ColDefinition> GetColumns()
        {
            var columns = new List<ColDefinition>(Columns.Count);
            for (int i = 0; i < Columns.Count; i++)
            {
                var col = Columns[i];
                var colDef = new ColDefinition(col);
                colDef.Editable = IsEditable(col);
                colDef.ColumnId = i.ToString();
                columns.Add(colDef);
            }

            return columns;
        }

        protected bool IsEditable(ColumnEntity column) => column.ControlStates.Contains(ControlState.Editable);
        protected bool IsHidden(ColumnEntity column) => column.ControlStates.Contains(ControlState.Hidden);
    }
}
