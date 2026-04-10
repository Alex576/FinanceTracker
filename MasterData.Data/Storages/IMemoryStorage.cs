using MasterData.Data.DBContext;

namespace MasterData.Data.Storages
{
    public interface IMemoryStorage<TData>
    {
        void Clear();
        TData Get(int id);
        Task LoadAsync(MasterDataContext context);
        void Update();
        void Update(List<TData> tiles);
    }
}