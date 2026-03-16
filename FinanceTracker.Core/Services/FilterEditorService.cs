using FinanceTracker.Core.Builders;
using FinanceTracker.Core.Builders.Forms;
using FinanceTracker.Core.Builders.Layouts;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Models.LayoutEntities;
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
    public class FilterEditorService : ILayoutItemService
    {
        private readonly IServiceProvider m_ServiceProvider;
        private readonly TileContextService m_FinanceContextService;
        private readonly ILayoutService m_LayoutService;

        public FilterEditorService(IServiceProvider serviceProvider, TileContextService financeContextService, ILayoutService layoutService)
        {
            m_ServiceProvider = serviceProvider;
            m_FinanceContextService = financeContextService;
            m_LayoutService = layoutService;
        }

        public async Task<OperationResult> DeleteItem(TileCode tileCode, string controlId)
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

        public async Task<FormModel> GetForm(TileCode tileCode, string? itemId, EditorType type)
        {


            switch (type)
            {
                case EditorType.Filter:
                case EditorType.Form:
                    {
                        var layoutBuilder = new FormLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<LayoutEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var control = layoutData.FormControls.FirstOrDefault(x => x.Id == itemId) ?? new();
                        var formBuilder = new FormEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.GetFormLayout(tileCode, control);
                    }
                case EditorType.Grid:
                    {
                        var layoutBuilder = new GridLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<GridLayoutEntity>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var column = layoutData.Columns.FirstOrDefault(x => x.ColumnId == itemId) ?? new();
                        var formBuilder = new GridEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.GetFormLayout(tileCode, column);
                    }
                default:
                    throw new NotImplementedException();
            }

            //return await formBuilder.GetFormLayout(tileCode, control);//todo split methods on editors and non editors methods
        }

        public async Task<OperationResultData<LayoutEditor>> SaveForm(SaveFormModel model)
        {
            OperationResult operationResult;
            switch (model.Type)
            {
                case EditorType.Filter:
                case EditorType.Form:
                    {
                        var layoutBuilder = new FormLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(model.TileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)model.TileCode);
                        var layoutData = JsonConvert.DeserializeObject<LayoutEditorModel>(layout?.LayoutJson ?? "") ?? new(model.TileCode);
                        var control = layoutData.FormControls.FirstOrDefault(x => x.Id == model.ItemId) ?? new();
                        var formBuilder = new FormEditorBuilder(m_FinanceContextService, formLayout);
                        operationResult = await formBuilder.SaveForm(model.TileCode, control, model);
                    }
                    break;
                case EditorType.Grid:
                    {
                        var layoutBuilder = new GridLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(model.TileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)model.TileCode);
                        var layoutData = JsonConvert.DeserializeObject<GridLayoutEntity>(layout?.LayoutJson ?? "") ?? new(model.TileCode);
                        var column = layoutData.Columns.FirstOrDefault(x => x.ColumnId == model.ItemId) ?? new();
                        var formBuilder = new GridEditorBuilder(m_FinanceContextService, formLayout);
                        operationResult = await formBuilder.SaveForm(model.TileCode, column, model);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }

            //var operationResult = await formBuilder.SaveForm(model.TileCode, control, model);
            var layoutTile = await m_FinanceContextService.GetLayoutTile((int)model.TileCode);
            var newLayout = await m_LayoutService.GetLayoutEditor((ToolCode)layoutTile.ToolCode);
            return new OperationResultData<LayoutEditor>(operationResult, newLayout);
        }

        public async Task<FormModel> UpdateForm(FormValueModel model)
        {
            switch (model.Type)
            {
                case EditorType.Filter:
                case EditorType.Form:
                    {
                        var layoutBuilder = new FormLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(model.TileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)model.TileCode);
                        var layoutData = JsonConvert.DeserializeObject<LayoutEditorModel>(layout?.LayoutJson ?? "") ?? new(model.TileCode);
                        var control = layoutData.FormControls.FirstOrDefault(x => x.Id == model.ItemId) ?? new();
                        var formBuilder = new FormEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.UpdateFormLayout(model.TileCode, control, model);
                    }
                case EditorType.Grid:
                    {
                        var layoutBuilder = new GridLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(model.TileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)model.TileCode);
                        var layoutData = JsonConvert.DeserializeObject<GridLayoutEntity>(layout?.LayoutJson ?? "") ?? new(model.TileCode);
                        var column = layoutData.Columns.FirstOrDefault(x => x.ColumnId == model.ItemId) ?? new();
                        var formBuilder = new GridEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.UpdateFormLayout(model.TileCode, column, model);
                    }
                default:
                    throw new NotImplementedException();
            }
            //return await formBuilder.UpdateFormLayout(model.TileCode, control, model);//todo split methods on editors and non editors methods
        }
    }
}
