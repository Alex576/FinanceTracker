using FinanceTracker.Data.DBContext;
using FinanceTracker.Data.DBModels;
using FinanceTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Data.Services
{
    public class TileContextService : FinanceContextServiceBase
    {
        private readonly string GET_TILE_CHILDREN = @"WITH CTE AS (
                    SELECT [tileCode], [name], [toolCode], [order], [type], [parentTileCode] FROM [dbo].[Tiles] WHERE [tileCode] IN ({0})
                    UNION ALL
                    SELECT [c].[tileCode], [c].[name], [c].[toolCode], [c].[order], [c].[type], [c].[parentTileCode] FROM [dbo].[Tiles] AS c
                    INNER JOIN CTE t ON [c].[parentTileCode] = [t].[tileCode]
                )
                SELECT * FROM CTE";

        private readonly string GET_LAYOUT_TILE = $@"WITH CTE AS (
                    SELECT [tileCode], [name], [toolCode], [order], [type], [parentTileCode] FROM [dbo].[Tiles] WHERE [tileCode] IN ({{0}})
                    UNION ALL
                    SELECT [c].[tileCode], [c].[name], [c].[toolCode], [c].[order], [c].[type], [c].[parentTileCode] FROM [dbo].[Tiles] AS c
                    INNER JOIN CTE t ON [c].[tileCode] = [t].[parentTileCode]
                )
                SELECT TOP(1) * FROM CTE WHERE [type] = {(int)TileTypeCode.Layout}";


        public TileContextService(FinanceTrackerContext context) : base(context)
        {
        }

        public IAsyncEnumerable<Tile> GetTilesChildren(List<int> tiles)
        {
            return m_Context.Tiles.FromSqlRaw(GET_TILE_CHILDREN, string.Join(",", tiles)).AsAsyncEnumerable();
        }

        public async Task<Tile> GetLayoutTile(int tileCode)
        {
            return await m_Context.Tiles.FromSqlRaw(GET_LAYOUT_TILE, tileCode.ToString()).AsAsyncEnumerable().FirstOrDefaultAsync() ?? throw new Exception($"Failed to find layout tile with child code = {tileCode}");
        }
    }
}
