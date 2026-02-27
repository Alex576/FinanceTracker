using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormEditorController : ControllerBase
    {
        private readonly IFormEditorService m_FormEditorService;

        public FormEditorController(IFormEditorService formEditorService)
        {
            m_FormEditorService = formEditorService;
        }

        [HttpPost("[action]")]
        public async Task<FormModel> GetForm(FormEditorModel model)
        {
            return await m_FormEditorService.GetForm(model.TileCode);
        }
    }
}
