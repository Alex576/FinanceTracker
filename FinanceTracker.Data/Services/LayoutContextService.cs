using FinanceTracker.Data.DBContext;
using FinanceTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Data.Services
{
    public class LayoutContextService : FinanceContextServiceBase
    {
        public LayoutContextService(FinanceTrackerContext context) : base(context)
        {
        }

        public async Task<LayoutModel<JsonType>?> GetLayout<JsonType>(int tileCode) where JsonType : class
        {
            var layout = await m_Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == tileCode);
            return layout == null ? null : new LayoutModel<JsonType>(layout);
        }
    }
}
