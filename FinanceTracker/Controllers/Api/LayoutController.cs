using FinanceTracker.Core.Models.FullScreenModels;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LayoutController : ControllerBase
    {
        private readonly ILayoutService m_LayoutService;

        public LayoutController(ILayoutService layoutService)
        {
            m_LayoutService = layoutService;
        }

        [HttpPost("[action]")]
        public async Task<LayoutManagementModel> GetLayoutManagement()
        {
            return await m_LayoutService.GetLayoutManagement();
        }

        [HttpPost("[action]")]
        public async Task<LayoutEditor> GetLayoutEditor(GetLayoutEditorModel model)
        {
            return await m_LayoutService.GetLayoutEditor(model.ToolCode);
        }

        [HttpPost("[action]")]
        public async Task<OperationResult> RemoveElement(RemoveLayoutItemModel model)
        {
            return await m_LayoutService.RemoveElement(model.TileCode, model.ItemId, model.Type);
        }

        [HttpPost("[action]")]
        public async Task<FullScreenFormEditorModel> GetForm(FullScreenFormModel model)
        {
            return await m_LayoutService.GetForm(model.TileCode, model.Controls, model.FormValueModel);
        }


    }
}
