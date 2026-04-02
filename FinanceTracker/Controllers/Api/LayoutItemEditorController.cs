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
    public class LayoutItemEditorController : ControllerBase
    {
        private readonly ILayoutItemService m_LayoutItemService;

        public LayoutItemEditorController(ILayoutItemService layoutItemService)
        {
            m_LayoutItemService = layoutItemService;
        }

        [HttpPost("[action]")]
        public async Task<FormModel> GetForm(FormEditorModel model)
        {
            return await m_LayoutItemService.GetForm(model.TileCode, model.ItemId, model.Type);
        }

        [HttpPost("[action]")]
        public async Task<FormModel> UpdateForm(LayoutItemFormEditorModel model)
        {
            return await m_LayoutItemService.UpdateForm(model.TileCode, model.ItemId, model.Type, model.Value);
        }

        [HttpPost("[action]")]
        public async Task<OperationResultData<LayoutEditor>> SaveForm(LayoutItemFormSaveEditorModel model)
        {
            return await m_LayoutItemService.SaveForm(model.TileCode, model.ItemId, model.Type, model.Value);
        }

        [HttpPost("[action]")]
        public async Task<OperationResult> RemoveItem(RemoveItemModel model)
        {
            return await m_LayoutItemService.RemoveItem(model.TileCode, model.ItemId, model.Type);
        }
    }
}
