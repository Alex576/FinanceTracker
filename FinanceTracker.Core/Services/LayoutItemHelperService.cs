using FinanceTracker.Core.Builders;
using FinanceTracker.Core.Builders.Layouts;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.DashboardEditor;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.Models;
using FinanceTracker.Data.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Services
{
    public class LayoutItemHelperService
    {
        private readonly TileContextService m_FinanceContextService;

        public LayoutItemHelperService(TileContextService financeContextService)
        {
            m_FinanceContextService = financeContextService;
        }
        public async Task<FormModel> GetLayoutItemFormAsync(TileCode tileCode, string? itemId)
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
                        return await formBuilder.GetFormLayout(tileCode, control);
                    }
                case TileTypeCode.Grid:
                    {
                        var layoutBuilder = new GridLayoutEditorBuilder(m_FinanceContextService);
                        var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
                        var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
                        var layoutData = JsonConvert.DeserializeObject<GridEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
                        var column = layoutData.GridEntity.Layout.Columns.FirstOrDefault(x => ItemCodeHelper.GetItemCode(x) == itemId) ?? new();
                        var formBuilder = new FormGridEditorBuilder(m_FinanceContextService, formLayout);
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

        public async Task<FormModel> UpdateLayoutItemFormAsync(TileCode tileCode, string? itemId, FormValueModel value)
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
                        var formBuilder = new FormGridEditorBuilder(m_FinanceContextService, formLayout);
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
    }
}
