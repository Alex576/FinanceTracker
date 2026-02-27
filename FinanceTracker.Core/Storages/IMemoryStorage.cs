using FinanceTracker.Data.DBModels;

namespace FinanceTracker.Core.Storages
{
    public interface IMemoryStorage<TData>
    {
        void Clear();
        TData Get(int id);
        void Load();
        void Update();
        void Update(List<TData> tiles);
    }
}