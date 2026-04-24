using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Data.Storages
{
    public interface IFinanceTrackerCache
    {
        public TranslationStorage GetTranslationStorage();
    }
}
