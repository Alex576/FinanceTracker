using FinanceTracker.Core.Storages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.Core.Cache
{
    public class Cache : ICache
    {
        private readonly IMemoryCache m_MemoryCache;
        private readonly IServiceProvider m_ServiceProvider;

        public Cache(IMemoryCache memoryCache, IServiceProvider serviceProvider)
        {
            m_MemoryCache = memoryCache;
            m_ServiceProvider = serviceProvider;
        }


        public TIleStorage GetTileStorage()
        {
            if (m_MemoryCache.TryGetValue<TIleStorage>(CacheKeys.TileStorageKey, out var storage) && storage != null)
                return storage;
            storage = ActivatorUtilities.CreateInstance<TIleStorage>(m_ServiceProvider);
            storage.Load();
            m_MemoryCache.Set(CacheKeys.TileStorageKey, storage);
            return storage;
        }
    }
}
