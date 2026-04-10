using FinanceTracker.Core.Storages;
using MasterData.Data.DBContext;
using MasterData.Data.Services;
using MasterData.Data.Storages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.Core.Cache
{
    public class Cache : ICache
    {
        private readonly IMemoryCache m_MemoryCache;
        private readonly IServiceScopeFactory m_ServiceScopeFactory;
        //private readonly IServiceProvider m_ServiceProvider;

        public Cache(IMemoryCache memoryCache, IServiceScopeFactory serviceScopeFactory)
        {
            m_MemoryCache = memoryCache;
            m_ServiceScopeFactory = serviceScopeFactory;
            //m_ServiceProvider = serviceProvider;
        }

        public ObjectStorage GetObjectStorage()
        {
            if (m_MemoryCache.TryGetValue<ObjectStorage>(CacheKeys.ObjectStorageKey, out var storage) && storage != null)
                return storage;
            storage = new ObjectStorage();// ActivatorUtilities.CreateInstance<ObjectStorage>(m_ServiceProvider);
            using var scope = m_ServiceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MasterDataContext>();
            storage.LoadAsync(context).GetAwaiter().GetResult();
            m_MemoryCache.Set(CacheKeys.ObjectStorageKey, storage);
            return storage;
        }

        //public TIleStorage GetTileStorage()
        //{
        //    if (m_MemoryCache.TryGetValue<TIleStorage>(CacheKeys.TileStorageKey, out var storage) && storage != null)
        //        return storage;
        //    storage = ActivatorUtilities.CreateInstance<TIleStorage>(m_ServiceProvider);
        //    storage.Load();
        //    m_MemoryCache.Set(CacheKeys.TileStorageKey, storage);
        //    return storage;
        //}
    }
}
