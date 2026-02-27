using FinanceTracker.Data.DBContext;
using FinanceTracker.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Storages
{
    public class TIleStorage : IMemoryStorage<Tile>
    {
        private readonly FinanceTrackerContext m_Context;

        private readonly Dictionary<int, Tile> m_TilesMap = new Dictionary<int, Tile>();
        public TIleStorage(FinanceTrackerContext context)
        {
            m_Context = context;
        }

        public void Load()
        {
            m_TilesMap.Clear();
            var allTiles = m_Context.Tiles.ToList();
            foreach (var tile in allTiles)
            {
                m_TilesMap.Add(tile.Id, tile);
            }
        }

        public void Clear()
        {
            m_TilesMap.Clear();
        }

        public Tile Get(int id)
        {
            return m_TilesMap[id];
        }

        public void Update()
        {

        }

        public void Update(List<Tile> tiles)
        {

        }
    }
}
