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
    public class GridEditorController : ControllerBase
    {
        //private readonly IGridEditorService m_GridEditorService;

        //public GridEditorController(IGridEditorService gridEditorService)
        //{
        //    m_GridEditorService = gridEditorService;
        //}

        //[HttpPost("[action]")]
        //public async Task<FormModel> GetForm(FormEditorModel model)
        //{
        //    return await m_GridEditorService.GetForm(model.TileCode, model.ItemId);
        //}

        //[HttpPost("[action]")]
        //public async Task<FormModel> UpdateForm(LayoutItemFormEditorModel model)
        //{
        //    return await m_GridEditorService.UpdateForm(model.TileCode, model.ItemId, model.Type, model.Value);
        //}

        //[HttpPost("[action]")]
        //public async Task<OperationResultData<LayoutEditor>> SaveForm(LayoutItemFormSaveEditorModel model)
        //{
        //    return await m_GridEditorService.SaveForm(model.TileCode, model.ItemId, model.Type, model.Value);
        //}
    }
}
