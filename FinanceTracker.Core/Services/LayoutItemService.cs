using FinanceTracker.Core.Builders;
using FinanceTracker.Core.Builders.Layouts;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinanceTracker.Core.Services
{
    public class LayoutItemService : ILayoutItemService
    {
        private readonly IServiceProvider m_ServiceProvider;
        private readonly TileContextService m_FinanceContextService;
        private readonly ILayoutService m_LayoutService;

        public LayoutItemService(IServiceProvider serviceProvider, TileContextService financeContextService, ILayoutService layoutService)
        {
            m_ServiceProvider = serviceProvider;
            m_FinanceContextService = financeContextService;
            m_LayoutService = layoutService;
        }

        public async Task<OperationResult> RemoveItem(TileCode tileCode, string controlId, EditorType type)
        {
            var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
            if (layout == null)
                return new OperationResult(ResultCode.Error, $"Failed to find layout with tile code = {tileCode}");
            switch (type)
            {
                case EditorType.Filter:
                case EditorType.Form:
                    {
                        if (!layout.LayoutJson.TryParse<LayoutEditorModel>(out var layoutData))
                            return new OperationResult(ResultCode.Error, $"Failed to parse layout with id = {layout.Id}");
                        var index = layoutData.FormControls.FindIndex(x => ItemCodeHelper.GetItemCode(x) == controlId);
                        if (index >= 0)
                        {
                            layoutData.FormControls.RemoveAt(index);
                            layout.LayoutJson = JsonConvert.SerializeObject(layoutData);
                            m_FinanceContextService.Context.Layouts.Update(layout);
                            await m_FinanceContextService.Context.SaveChangesAsync();
                        }
                        break;
                    }
                case EditorType.Grid:
                    {
                        if (!layout.LayoutJson.TryParse<GridEditorModel>(out var layoutData))
                            return new OperationResult(ResultCode.Error, $"Failed to parse layout with id = {layout.Id}");
                        var index = layoutData.GridEntity.Layout.Columns.FindIndex(x => ItemCodeHelper.GetItemCode(x) == controlId);
                        if (index >= 0)
                        {
                            layoutData.GridEntity.Layout.Columns.RemoveAt(index);
                            layout.LayoutJson = JsonConvert.SerializeObject(layoutData);
                            m_FinanceContextService.Context.Layouts.Update(layout);
                            await m_FinanceContextService.Context.SaveChangesAsync();
                        }
                        break;
                    }
                default:
                    throw new NotImplementedException();
            }
            return new OperationResult(ResultCode.Success);

        }

        public async Task<FormModel> GetForm(TileCode tileCode, string? itemId, EditorType type)
        {
            switch (type)
            {//todo dashboard editor
                case EditorType.Filter:
                case EditorType.Form:
                    {
                        var layoutBuilder = new FormLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<LayoutEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var control = layoutData.FormControls.FirstOrDefault(x => ItemCodeHelper.GetItemCode(x) == itemId) ?? new();
                        var formBuilder = new FormEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.GetFormLayout(tileCode, control);
                    }
                case EditorType.Grid:
                    {
                        var layoutBuilder = new GridLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<GridEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var column = layoutData.GridEntity.Layout.Columns.FirstOrDefault(x => ItemCodeHelper.GetItemCode(x) == itemId) ?? new();
                        var formBuilder = new GridEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.GetFormLayout(tileCode, column);
                    }
                default:
                    throw new NotImplementedException();
            }

        }

        public async Task<FormModel> UpdateForm(TileCode tileCode, string? itemId, EditorType type, FormValueModel value)
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
                        var control = layoutData.FormControls.FirstOrDefault(x => ItemCodeHelper.GetItemCode(x) == itemId) ?? new();
                        var formBuilder = new FormEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.UpdateFormLayout(tileCode, control, value);
                    }
                case EditorType.Grid:
                    {
                        var layoutBuilder = new GridLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<GridEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var column = layoutData.GridEntity.Layout.Columns.FirstOrDefault(x => ItemCodeHelper.GetItemCode(x) == itemId) ?? new();
                        var formBuilder = new GridEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.UpdateFormLayout(tileCode, column, value);
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public async Task<OperationResultData<LayoutEditor>> SaveForm(TileCode tileCode, string? itemId, EditorType type, FormValueModel value)
        {
            OperationResult operationResult;
            switch (type)
            {
                case EditorType.Filter:
                case EditorType.Form:
                    {
                        var layoutBuilder = new FormLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<LayoutEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var control = layoutData.FormControls.FirstOrDefault(x => ItemCodeHelper.GetItemCode(x) == itemId) ?? new();
                        var formBuilder = new FormEditorBuilder(m_FinanceContextService, formLayout);
                        operationResult = await formBuilder.SaveForm(tileCode, control, value);
                    }
                    break;
                case EditorType.Grid:
                    {
                        var layoutBuilder = new GridLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<GridEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var column = layoutData.GridEntity.Layout.Columns.FirstOrDefault(x => ItemCodeHelper.GetItemCode(x) == itemId) ?? new();
                        var formBuilder = new GridEditorBuilder(m_FinanceContextService, formLayout);
                        operationResult = await formBuilder.SaveForm(tileCode, column, value);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }

            var layoutTile = await m_FinanceContextService.GetLayoutTile((int)tileCode);
            var newLayout = await m_LayoutService.GetLayoutEditor((ToolCode)layoutTile.ToolCode);
            return new OperationResultData<LayoutEditor>(operationResult, newLayout);
        }
    }
}
