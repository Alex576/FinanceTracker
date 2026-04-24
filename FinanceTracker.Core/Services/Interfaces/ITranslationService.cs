using FinanceTracker.Core.Models;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface ITranslationService
    {
        Task<List<TranslationModel>> GetAllTranslations(int languageCode);
    }
}