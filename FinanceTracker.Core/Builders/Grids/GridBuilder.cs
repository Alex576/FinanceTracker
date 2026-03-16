using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Builders.Grids
{
    public abstract class GridBuilder<T>
    {
        protected readonly GridEntityLayout m_GridLayout;

        protected List<ColumnEntity> Columns => m_GridLayout.Columns;

        public GridBuilder(GridEntityLayout gridLayout)
        {
            m_GridLayout = gridLayout;
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
                row.Data.Add(GetCellData(col, data));
            }

            return row;
        }

        protected abstract JToken? GetCellData(ColumnEntity col, T data);

        protected List<ColDefinition> GetColumns()
        {
            var columns = new List<ColDefinition>(Columns.Count);
            foreach (var col in Columns)
            {
                columns.Add(new ColDefinition(col));
            }

            return columns;
        }
    }
}
