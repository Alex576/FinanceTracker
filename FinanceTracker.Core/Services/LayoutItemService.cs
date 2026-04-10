using FinanceTracker.Core.Builders;
using FinanceTracker.Core.Builders.Layouts;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.DashboardEditor;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.Models;
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

        public async Task<OperationResult> RemoveItem(TileCode tileCode, string itemId)
        {
            var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
            if (layout == null)
                return new OperationResult(ResultCode.Error, $"Failed to find layout with tile code = {tileCode}");
            var type = await m_FinanceContextService.Context.Tiles.FirstAsync(x => x.TileCode == (int)tileCode);
            switch ((TileTypeCode)type.Type)
            {
                case TileTypeCode.Filter:
                case TileTypeCode.Form:
                    {
                        if (!layout.LayoutJson.TryParse<LayoutEditorModel>(out var layoutData))
                            return new OperationResult(ResultCode.Error, $"Failed to parse layout with id = {layout.Id}");
                        var index = layoutData.FormControls.FindIndex(x => ItemCodeHelper.GetItemCode(x) == itemId);
                        if (index >= 0)
                        {
                            layoutData.FormControls.RemoveAt(index);
                            layout.LayoutJson = JsonConvert.SerializeObject(layoutData);
                            m_FinanceContextService.Context.Layouts.Update(layout);
                            await m_FinanceContextService.Context.SaveChangesAsync();
                        }
                        break;
                    }
                case TileTypeCode.Grid:
                    {
                        if (!layout.LayoutJson.TryParse<GridEditorModel>(out var layoutData))
                            return new OperationResult(ResultCode.Error, $"Failed to parse layout with id = {layout.Id}");
                        var index = layoutData.GridEntity.Layout.Columns.FindIndex(x => ItemCodeHelper.GetItemCode(x) == itemId);
                        if (index >= 0)
                        {
                            layoutData.GridEntity.Layout.Columns.RemoveAt(index);
                            layout.LayoutJson = JsonConvert.SerializeObject(layoutData);
                            m_FinanceContextService.Context.Layouts.Update(layout);
                            await m_FinanceContextService.Context.SaveChangesAsync();
                        }
                        break;
                    }
                case TileTypeCode.Dashboard:
                    {
                        if (!layout.LayoutJson.TryParse<DashboardEditorModel>(out var layoutData))
                            return new OperationResult(ResultCode.Error, $"Failed to parse layout with id = {layout.Id}");
                        var index = layoutData.Items.FindIndex(x => x.Id == itemId);
                        if (index >= 0)
                        {
                            layoutData.Items.RemoveAt(index);
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

        public async Task<FormModel> GetForm(TileCode tileCode, string? itemId)
        {
            var type = await m_FinanceContextService.Context.Tiles.FirstAsync(x => x.TileCode == (int)tileCode);

            switch ((TileTypeCode)type.Type)
            {//todo dashboard editor
                case TileTypeCode.Filter:
                case TileTypeCode.Form:
                    {
                        var layoutBuilder = new FormLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<LayoutEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var control = layoutData.FormControls.FirstOrDefault(x => ItemCodeHelper.GetItemCode(x) == itemId) ?? new();
                        var formBuilder = new FormEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.GetFormLayout(tileCode, control);
                    }
                case TileTypeCode.Grid:
                    {
                        var layoutBuilder = new GridLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<GridEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var column = layoutData.GridEntity.Layout.Columns.FirstOrDefault(x => ItemCodeHelper.GetItemCode(x) == itemId) ?? new();
                        var formBuilder = new GridEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.GetFormLayout(tileCode, column);
                    }
                case TileTypeCode.Dashboard:
                    {
                        var layoutBuilder = new DashboardLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<DashboardEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var item = layoutData.Items.FirstOrDefault(x => x.Id == itemId) ?? new();
                        var formBuilder = new DashboardEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.GetFormLayout(tileCode, item);
                    }
                default:
                    throw new NotImplementedException();
            }

        }

        public async Task<FormModel> UpdateForm(TileCode tileCode, string? itemId, FormValueModel value)
        {
            var type = await m_FinanceContextService.Context.Tiles.FirstAsync(x => x.TileCode == (int)tileCode);

            switch ((TileTypeCode)type.Type)
            {
                case TileTypeCode.Filter:
                case TileTypeCode.Form:
                    {
                        var layoutBuilder = new FormLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<LayoutEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var control = layoutData.FormControls.FirstOrDefault(x => ItemCodeHelper.GetItemCode(x) == itemId) ?? new();
                        var formBuilder = new FormEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.UpdateFormLayout(tileCode, control, value);
                    }
                case TileTypeCode.Grid:
                    {
                        var layoutBuilder = new GridLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<GridEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var column = layoutData.GridEntity.Layout.Columns.FirstOrDefault(x => ItemCodeHelper.GetItemCode(x) == itemId) ?? new();
                        var formBuilder = new GridEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.UpdateFormLayout(tileCode, column, value);
                    }
                case TileTypeCode.Dashboard:
                    {
                        var layoutBuilder = new DashboardLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<DashboardEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var item = layoutData.Items.FirstOrDefault(x => x.Id == itemId) ?? new();
                        var formBuilder = new DashboardEditorBuilder(m_FinanceContextService, formLayout);
                        return await formBuilder.UpdateFormLayout(tileCode, item, value);
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public async Task<OperationResultData<LayoutEditor>> SaveForm(TileCode tileCode, string? itemId, FormValueModel value)
        {
            var type = await m_FinanceContextService.Context.Tiles.FirstAsync(x => x.TileCode == (int)tileCode);

            OperationResult operationResult;
            switch ((TileTypeCode)type.Type)
            {
                case TileTypeCode.Filter:
                case TileTypeCode.Form:
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
                case TileTypeCode.Grid:
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
                case TileTypeCode.Dashboard:
                    {
                        var layoutBuilder = new DashboardLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<DashboardEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var item = layoutData.Items.FirstOrDefault(x => x.Id == itemId) ?? new();
                        var formBuilder = new DashboardEditorBuilder(m_FinanceContextService, formLayout);
                        operationResult = await formBuilder.SaveForm(tileCode, item, value);
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
