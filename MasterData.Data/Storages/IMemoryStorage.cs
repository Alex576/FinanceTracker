using MasterData.Data.DBContext;
using MasterData.Data.Models;

namespace MasterData.Data.Storages
{
    public interface IMemoryStorage<TData>
    {
        void Clear();
        TData? Get(int id);
        List<TData> Get(List<int> ids);
        Task LoadAsync(MasterDataContext context);
        void Update();
        void Update(List<TData> tiles);
    }
}