using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Services
{
    public class SessionService: ISessionService
    {
        public int CurrentUser { get; set; }
    }
}
