using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancesController : ControllerBase
    {
        private readonly IFinancesService m_FinancesService;

        public FinancesController(IFinancesService financesService)
        {
            m_FinancesService = financesService;
        }

        [HttpPost("[action]")]
        public async Task<DashboardLayout> GetFinances(GetGridLayoutModel model)
        {
            return await m_FinancesService.GetFinances(model.ToolCode, model.Filters);
        }

        [HttpPost("[action]")]
        public async Task<List<FormControl>> GetFilters(GetFilterModel model)
        {
            return await m_FinancesService.GetFilters(model.ToolCode);
        }
    }
}
