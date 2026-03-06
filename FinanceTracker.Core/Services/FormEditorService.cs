using FinanceTracker.Core.Builders;
using FinanceTracker.Core.Builders.Layouts;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.DBContext;
using FinanceTracker.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FinanceTracker.Core.Services
{
    public class FormEditorService : IFormEditorService
    {
        private readonly IServiceProvider m_ServiceProvider;
        private readonly FinanceContextService m_FinanceContextService;
        private readonly ILayoutService m_LayoutService;

        public FormEditorService(IServiceProvider serviceProvider, FinanceContextService financeContextService, ILayoutService layoutService)
        {
            m_ServiceProvider = serviceProvider;
            m_FinanceContextService = financeContextService;
            m_LayoutService = layoutService;
        }

        public async Task<OperationResult> DeleteControl(TileCode tileCode, string controlId)  
        {
            var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
            if (layout == null)
                return new OperationResult(ResultCode.Error, $"Failed to find layout with tile code = {tileCode}");
            if (!layout.LayoutJson.TryParse<LayoutEditorModel>(out var layoutData))
                return new OperationResult(ResultCode.Error, $"Failed to pares layout with id = {layout.Id}");
            var index = layoutData.FormControls.FindIndex(x => x.Id == controlId);
            if (index >= 0)
            {
                layoutData.FormControls.RemoveAt(index);
                layout.LayoutJson = JsonConvert.SerializeObject(layoutData);
                m_FinanceContextService.Context.Layouts.Update(layout);
                await m_FinanceContextService.Context.SaveChangesAsync();
            }
            return new OperationResult(ResultCode.Success);
        }

        public async Task<FormModel> GetForm(TileCode tileCode, string? itemId)
        {
            var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
            var layoutData = JsonConvert.DeserializeObject<LayoutEditorModel>(layout?.LayoutJson ?? "") ?? new();
            var control = layoutData.FormControls.FirstOrDefault(x => x.Id == itemId) ?? new();
            var layoutBuilder = new LayoutEditorBuilder(m_FinanceContextService);
            var formLayout = await layoutBuilder.GetFormEditorLayout(tileCode);
            var formBuilder = new FormEditorBuilder(m_FinanceContextService, formLayout);

            return await formBuilder.GetFormLayout(tileCode, control);//todo split methods on editors and non editors methods
        }

        public async Task<OperationResultData<LayoutEditor>> SaveForm(SaveFormModel model)
        {
            var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)model.TileCode);
            var layoutData = JsonConvert.DeserializeObject<LayoutEditorModel>(layout?.LayoutJson ?? "") ?? new();
            var control = layoutData.FormControls.FirstOrDefault(x => x.Id == model.ItemId) ?? new();

            var layoutBuilder = new LayoutEditorBuilder(m_FinanceContextService, model);
            var formLayout = await layoutBuilder.GetFormEditorLayout(model.TileCode);
            var formBuilder = new FormEditorBuilder(m_FinanceContextService, formLayout);

            var operationResult = await formBuilder.SaveForm(model.TileCode, control, model);
            var layoutTile = await m_FinanceContextService.GetLayoutTile((int)model.TileCode);
            var newLayout = await m_LayoutService.GetLayoutEditor((ToolCode)layoutTile.ToolCode);
            return new OperationResultData<LayoutEditor>(operationResult, newLayout);
        }

        public async Task<FormModel> UpdateForm(FormValueModel model)
        {
            var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)model.TileCode);
            var layoutData = JsonConvert.DeserializeObject<LayoutEditorModel>(layout?.LayoutJson ?? "") ?? new();
            var control = layoutData.FormControls.FirstOrDefault(x => x.Id == model.ItemId) ?? new();

            var layoutBuilder = new LayoutEditorBuilder(m_FinanceContextService, model);
            var formLayout = await layoutBuilder.GetFormEditorLayout(model.TileCode);
            var formBuilder = new FormEditorBuilder(m_FinanceContextService, formLayout);

            return await formBuilder.UpdateFormLayout(model.TileCode, control, model);//todo split methods on editors and non editors methods
        }
    }
}
