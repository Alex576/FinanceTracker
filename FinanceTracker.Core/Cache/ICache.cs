using FinanceTracker.Data.Storages;
using MasterData.Data.Storages;

namespace FinanceTracker.Core.Cache
{
    public interface ICache : IMasterDataCache, IFinanceTrackerCache
    {
        //public ObjectStorage GetObjectStorage();
    }
}
