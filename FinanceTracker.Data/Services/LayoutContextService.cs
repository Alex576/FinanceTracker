using FinanceTracker.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinanceTracker.Data.Services
{
    public class LayoutContextService : FinanceContextServiceBase
    {
        public LayoutContextService(FinanceTrackerContext context) : base(context)
        {
        }

        public async Task<JsonType> GetLayout<JsonType>(int tileCode) where JsonType : class
        {
            var layout = await m_Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == tileCode)
                ?? throw new NullReferenceException($"Failed to find layout with tileCode = {tileCode}");
            var layoutData = JsonConvert.DeserializeObject<JsonType>(layout.LayoutJson ?? "")
                ?? throw new NullReferenceException($"Failed to deserialize object with tileCode = {tileCode}");
            return layoutData;
        }

        public async Task<JsonType?> TryGetLayout<JsonType>(int tileCode) where JsonType : class
        {
            var layout = await m_Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == tileCode);

            return layout == null ? null : JsonConvert.DeserializeObject<JsonType>(layout.LayoutJson ?? "");
        }
    }
}
