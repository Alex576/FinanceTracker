using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapitalController : ControllerBase
    {
        private readonly ICapitalService m_CapitalService;

        public CapitalController(ICapitalService capitalItemEditor)
        {
            m_CapitalService = capitalItemEditor;
        }

        [HttpPost("[action]")]
        public async Task<FormModel> GetForm(CapitalFormModel model)
        {
            return await m_CapitalService.GetForm(model.TileCode, model.CapitalId);
        }

        [HttpPost("[action]")]
        public async Task<FormModel> UpdateForm(CapitalFormModel model)
        {
            ArgumentNullException.ThrowIfNull(model.Value);
            return await m_CapitalService.UpdateForm(model.TileCode, model.CapitalId, model.Value);
        }

        [HttpPost("[action]")]
        public async Task<OperationResultData<DashboardItem>> SaveForm(CapitalFormModel model)
        {
            ArgumentNullException.ThrowIfNull(model.Value);
            return await m_CapitalService.SaveForm(model.TileCode, model.CapitalId, model.Value);
        }

        [HttpPost("[action]")]
        public async Task<DashboardLayout> GetCapitals(GetGridLayoutModel model)
        {
            return await m_CapitalService.GetCapitals(model.ToolCode, model.Filters);
        }

        [HttpPost("[action]")]
        public async Task<List<FormControl>> GetFilters(GetFilterModel model)
        {
            return await m_CapitalService.GetFilters(model.ToolCode);
        }
    }
}
