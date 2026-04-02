using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapitalItemEditorController : ControllerBase
    {
        private readonly ICapitalItemEditor m_CapitalItemEditor;

        public CapitalItemEditorController(ICapitalItemEditor capitalItemEditor)
        {
            m_CapitalItemEditor = capitalItemEditor;
        }

        [HttpPost("[action]")]
        public async Task<FormModel> GetForm(CapitalFormModel model)
        {
            return await m_CapitalItemEditor.GetForm(model.TileCode, model.CapitalId);
        }

        [HttpPost("[action]")]
        public async Task<FormModel> GetUpdateForm(CapitalFormModel model)
        {
            ArgumentNullException.ThrowIfNull(model.Value);
            return await m_CapitalItemEditor.UpdateForm(model.TileCode, model.CapitalId, model.Value);
        }

        [HttpPost("[action]")]
        public async Task<OperationResultData<DashboardItem>> GetSaveForm(CapitalFormModel model)
        {
            ArgumentNullException.ThrowIfNull(model.Value);
            return await m_CapitalItemEditor.SaveForm(model.TileCode, model.CapitalId, model.Value);
        }
    }
}
