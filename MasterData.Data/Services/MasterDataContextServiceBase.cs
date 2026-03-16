using MasterData.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.Data.Services
{
    public class MasterDataContextServiceBase
    {
        protected readonly MasterDataContext m_Context;
        public MasterDataContext Context => m_Context;

        public MasterDataContextServiceBase(MasterDataContext context)
        {
            m_Context = context;
        }
    }
}
