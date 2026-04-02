using MasterData.Data.DBContext;
using MasterData.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterData.Data.Services
{
    public class ObjectContextService : MasterDataContextServiceBase
    {
        public ObjectContextService(MasterDataContext context) : base(context)
        {
        }

        public async Task<List<ObjectModel>> GetObjectsByClass(int classCode)
        {
            return await m_Context.ObjectEntities.Where(x => x.ClassCode == classCode).Select(x => new ObjectModel(x)).ToListAsync();
        }

        public async Task<List<ObjectModel>> GetObjects(List<int> objList)
        {
            return await m_Context.ObjectEntities.Where(x => objList.Contains(x.Id)).Select(x => new ObjectModel(x)).ToListAsync();
        }
    }
}
