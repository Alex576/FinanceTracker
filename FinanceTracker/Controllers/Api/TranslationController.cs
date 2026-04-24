using FinanceTracker.Core.Models;
using FinanceTracker.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService m_TranslationService;

        public TranslationController(ITranslationService translationService)
        {
            m_TranslationService = translationService;
        }

        [HttpPost("[action]")]
        public async Task<List<TranslationModel>> GetTranslations(int languageCode)
        {
            return await m_TranslationService.GetAllTranslations(languageCode);
        }
    }
}
