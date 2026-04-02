using FinanceTracker.Data.DBModels;
using Newtonsoft.Json;

namespace FinanceTracker.Data.Models
{
    public class LayoutModel<TData> where TData : class
    {
        public int TileCode { get; set; }
        public TData Layout { get; set; }

        public LayoutModel(Layout layout)
        {
            TileCode = layout.TileCode;
            Layout = JsonConvert.DeserializeObject<TData>(layout.LayoutJson ?? "") ?? Activator.CreateInstance<TData>();
        }
    }
}
