using FinanceTracker.Core.Builders;
using FinanceTracker.Core.Builders.Layouts;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Models.LayoutEntities;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Data.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Services
{
    public class GridEditorService : IGridEditorService
    {
        private readonly LayoutContextService m_LayoutContextService;
        private readonly TileContextService m_TileContextService;
        private readonly ILayoutService m_LayoutService;

        public GridEditorService(LayoutContextService layoutContextService, TileContextService tileContextService, ILayoutService layoutService)
        {
            m_LayoutContextService = layoutContextService;
            m_TileContextService = tileContextService;
            m_LayoutService = layoutService;
        }

        public async Task<FormModel> GetForm(TileCode tileCode, string? itemId)
        {
            var layout = await m_LayoutContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
            var layoutData = JsonConvert.DeserializeObject<GridEditorModel>(layout?.LayoutJson ?? "") ?? new(tileCode);
            var column = layoutData.GridEntity.Layout.Columns.FirstOrDefault(x => x.ColumnId == itemId) ?? new();
            var layoutBuilder = new GridLayoutEditorBuilder(m_TileContextService);
            var formLayout = layoutBuilder.GetFormEditorLayout(tileCode);
            var formBuilder = new GridEditorBuilder(m_LayoutContextService, formLayout);

            return await formBuilder.GetFormLayout(tileCode, column);
        }

        public async Task<OperationResultData<LayoutEditor>> SaveForm(SaveFormModel model)
        {
            var layout = await m_LayoutContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)model.TileCode);
            var layoutData = JsonConvert.DeserializeObject<GridEditorModel>(layout?.LayoutJson ?? "") ?? new(model.TileCode);
            var column = layoutData.GridEntity.Layout.Columns.FirstOrDefault(x => x.ColumnId == model.ItemId) ?? new();

            var layoutBuilder = new GridLayoutEditorBuilder(m_TileContextService);
            var formLayout = layoutBuilder.GetFormEditorLayout(model.TileCode);
            var formBuilder = new GridEditorBuilder(m_LayoutContextService, formLayout);

            var operationResult = await formBuilder.SaveForm(model.TileCode, column, model);
            var layoutTile = await m_TileContextService.GetLayoutTile((int)model.TileCode);
            var newLayout = await m_LayoutService.GetLayoutEditor((ToolCode)layoutTile.ToolCode);
            return new OperationResultData<LayoutEditor>(operationResult, newLayout);
        }

        public async Task<FormModel> UpdateForm(FormValueModel model)
        {
            var layout = await m_LayoutContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)model.TileCode);
            var layoutData = JsonConvert.DeserializeObject<GridEditorModel>(layout?.LayoutJson ?? "") ?? new(model.TileCode);
            var column = layoutData.GridEntity.Layout.Columns.FirstOrDefault(x => x.ColumnId == model.ItemId) ?? new();

            var layoutBuilder = new GridLayoutEditorBuilder(m_TileContextService);
            var formLayout = layoutBuilder.GetFormEditorLayout(model.TileCode);
            var formBuilder = new GridEditorBuilder(m_LayoutContextService, formLayout);

            return await formBuilder.UpdateFormLayout(model.TileCode, column, model);
        }
    }
}
