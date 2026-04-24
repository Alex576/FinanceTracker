using FinanceTracker.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Data.Services
{
    public class TranslationContextService : FinanceContextServiceBase
    {
        public TranslationContextService(FinanceTrackerContext context) : base(context)
        {
        }
    }
}
