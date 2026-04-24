using FinanceTracker.Core.Cache;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly ICache m_Cache;

        public TranslationService(ICache cache)
        {
            m_Cache = cache;
        }
        public Task<List<TranslationModel>> GetAllTranslations(int languageCode)
        {
            var translationStorage = m_Cache.GetTranslationStorage();
            return Task.FromResult(translationStorage.AllTranslations.Select(x => new TranslationModel() { Key = x.Key, Value = x.Value }).ToList());
        }
    }
}
