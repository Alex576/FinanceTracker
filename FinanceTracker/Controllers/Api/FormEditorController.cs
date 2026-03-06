using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Models;
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
            return await m_FormEditorService.GetForm(model.TileCode, model.ItemId);
        }

        [HttpPost("[action]")]
        public async Task<FormModel> UpdateForm(FormValueModel model)
        {
            return await m_FormEditorService.UpdateForm(model);
        }

        [HttpPost("[action]")]
        public async Task<OperationResultData<LayoutEditor>> SaveForm(SaveFormModel model)
        {
            return await m_FormEditorService.SaveForm(model);
        }

        [HttpPost("[action]")]
        public async Task<OperationResult> DeleteControl(DeleteControlModel model)
        {
            return await m_FormEditorService.DeleteControl(model.TileCode, model.ControlId);
        }
    }
}
