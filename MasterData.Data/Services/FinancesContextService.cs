using MasterData.Data.DBContext;
using MasterData.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterData.Data.Services
{
    public class FinancesContextService : MasterDataContextServiceBase
    {
        public FinancesContextService(MasterDataContext context) : base(context)
        {
        }

        public async Task<List<FinanceModel>> GetAllFinances()
        {
            var finances = await m_Context.FinanceItems.Select(x => new FinanceModel(x)).ToListAsync();
            return finances;
        }

        public async Task<List<FinanceModel>> GetCapitals(List<int>? objCodes = null, DateTime? from = null, DateTime? to = null)
        {
            var query = m_Context.FinanceItems.Where(x => x.FinanceType == (int)Models.FinanceType.Capital);
            if (from.HasValue)
                query = query.Where(x => x.DateFrom >= from.Value);
            if (to.HasValue)
                query = query.Where(x => x.DateTo <= to);
            if (objCodes != null && objCodes.Count > 0)
                query = query.Where(x => x.OptionsJson.ObjCodes.All(x => objCodes.Contains(x)));
            return await query.Select(x => new FinanceModel(x)).ToListAsync();
        }
    }
}
