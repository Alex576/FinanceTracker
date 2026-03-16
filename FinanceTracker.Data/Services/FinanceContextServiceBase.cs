using FinanceTracker.Data.DBContext;

namespace FinanceTracker.Data.Services
{
    public abstract class FinanceContextServiceBase
    {

        protected readonly FinanceTrackerContext m_Context;
        public FinanceTrackerContext Context => m_Context;
        public FinanceContextServiceBase(FinanceTrackerContext context)
        {
            m_Context = context;
        }

    }
}