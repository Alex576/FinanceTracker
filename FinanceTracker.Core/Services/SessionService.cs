using FinanceTracker.Core.Services.Interfaces;

namespace FinanceTracker.Core.Services
{
    public class SessionService : ISessionService
    {
        public int CurrentUser { get; set; }
    }
}
