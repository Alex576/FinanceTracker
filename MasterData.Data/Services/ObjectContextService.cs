using MasterData.Data.DBContext;
using MasterData.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.Data.Services
{
    public class ObjectContextService : MasterDataContextServiceBase
    {
        public ObjectContextService(MasterDataContext context) : base(context)
        {
        }

        public async Task<List<ObjectModel>> GetObjects(int classCode)
        {
            return await m_Context.ObjectEntities.Where(x => x.ClassCode == classCode).Select(x => new ObjectModel(x)).ToListAsync();
        }
    }
}
