using MasterData.Data.DBContext;
using MasterData.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.Data.Services
{
    public class FinancesContextService : MasterDataContextServiceBase
    {
        public FinancesContextService(MasterDataContext context) : base(context)
        {
        }

        public async Task<List<FinanceModel>> GetAllFinances(int capitalId)
        {
            var finances = await m_Context.Finances.Where(x => x.CapitalId == capitalId).Select(x => new FinanceModel(x)).ToListAsync();
            return finances;
        }
    }
}
