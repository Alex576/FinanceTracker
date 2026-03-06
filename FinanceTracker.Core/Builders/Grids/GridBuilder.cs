using FinanceTracker.Core.Models.Grid;

namespace FinanceTracker.Core.Builders.Grids
{
    public abstract class GridBuilder<TData> where TData : class
    {
        public GridBuilder() { }

        public virtual Grid GetLayout(List<TData> data)
        {
            var grid = new Grid();
            var layout = new Layout();
            layout.Cols = GetColumns(typeof(TData));
            grid.Rows = GetRows(data);
            grid.Layout = layout;

            return grid;
        }

        protected abstract List<List<object>> GetRows(List<TData> data);
        protected abstract List<ColDefinition> GetColumns(Type type);
    }
}
