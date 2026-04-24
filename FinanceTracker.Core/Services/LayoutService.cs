using FinanceTracker.Core.Builders.Filter;
using FinanceTracker.Core.Builders.Grids;
using FinanceTracker.Core.Builders.Layouts;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.FullScreenModels;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.DashboardEditor;
using FinanceTracker.Core.Models.LayoutEditor.FormEditorModels;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Models.LayoutEntities;
using FinanceTracker.Core.Models.LayoutPreviews;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinanceTracker.Core.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly TileContextService m_TileContextService;
        private readonly LayoutContextService m_LayoutContextService;

        public LayoutService(TileContextService financeContextService, LayoutContextService layoutContextService)
        {
            m_TileContextService = financeContextService;
            m_LayoutContextService = layoutContextService;
        }

        public async Task<LayoutManagementModel> GetLayoutManagement()
        {
            var tools = await m_TileContextService.Context.Tools.Where(x => x.Id != (int)ToolCode.Settings).ToListAsync();
            var model = new LayoutManagementModel();
            var toolItems = tools.Select(x => new Item() { Id = x.Id, Name = x.Name }).ToList();
            var builder = new LayoutFiltersBuilder([]);

            model.ToolFilter = builder.GetFilterControl("Tools", toolItems, Constants.ToolFilterId, TileItemCode.Tool);

            var tiles = model.ToolFilter.Value.TryParse(out int toolValue) ? await GetTileUnderTool(toolValue) : [];

            var tileItems = tiles.Select(x => new Item() { Id = (int)x.TileCode, Name = x.Name }).ToList();
            model.TileFilter = builder.GetFilterControl("Tiles", tileItems, Constants.TileFilterId, TileItemCode.Tile);

            return model;
        }

        private async Task<List<Tile>> GetTileUnderTool(int toolValue)
        {
            return await m_TileContextService.Context.Tiles.Where(x => x.ToolCode == toolValue).Select(x => new Tile(x)).ToListAsync();
        }

        public async Task<LayoutEditor> GetLayoutEditor(ToolCode toolCode)
        {
            var layoutEditor = new LayoutEditor();

            var tiles = await GetTileUnderTool((int)toolCode);
            var controlItems = tiles.Select(x => new Item() { Id = (int)x.TileCode, Name = x.Name }).ToList();
            var builder = new LayoutFiltersBuilder([]);

            layoutEditor.TileFilter = builder.GetFilterControl("Tiles", controlItems, Constants.TileFilterId, TileItemCode.Tile);// filterBuilder.GetFilterControl(controlItems, "Tiles", new ComboControlSettings() { AllowMultiselect = false }, );

            var layoutBuilder = new LayoutEditorBuilder(m_TileContextService);
            layoutEditor.LayoutItems = await GetLayoutEditorItems(await layoutBuilder.GetLayoutAsync(tiles));
            return layoutEditor;
        }

        private async Task<List<LayoutEntity>> GetLayoutEditorItems(LayoutPreview layoutPreview)
        {
            var layoutItems = new List<LayoutEntity>();
            foreach (var previewItem in layoutPreview.Previews)
            {
                var layout = await m_TileContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)previewItem.TileCode);
                var item = new LayoutEntity() { TileCode = previewItem.TileCode };

                switch (previewItem)
                {
                    case FilterPreview filterPreview:
                        {
                            var entity = new FilterLayoutEntity(previewItem.TileCode);
                            if (layout != null)
                            {
                                var filters = JsonConvert.DeserializeObject<LayoutEditorModel>(layout.LayoutJson ?? "") ?? new(previewItem.TileCode);
                                entity.Filters = filters.FormControls.Select(x => new FormControl(x)).ToList();
                            }
                            item.Data = entity;
                            break;
                        }

                    case GridPreview gridPreview:
                        {
                            var entity = new GridLayoutEntity(previewItem.TileCode);
                            if (layout != null)
                            {
                                var gridLayout = JsonConvert.DeserializeObject<GridEditorModel>(layout.LayoutJson ?? "") ?? new(previewItem.TileCode);
                                var gridBuilder = new GridEditorBuilder(new GridLayoutBuilder().GetGridEditorLayout());
                                entity.GridEditor = new GridEditorEntity() { GridEntity = gridBuilder.GetLayout(gridLayout.GridEntity.Layout.Columns) };
                            }
                            item.Data = entity;
                            break;
                        }
                    case DashboardPreview dashboardPreview:
                        {
                            var entity = new DashboardLayoutEntity(previewItem.TileCode);
                            if (layout != null)
                            {
                                var dashboardLayout = JsonConvert.DeserializeObject<DashboardEditorModel>(layout.LayoutJson ?? "") ?? new(previewItem.TileCode);
                                var gridBuilder = new DashboardLayout();
                                gridBuilder.Options.CanAdd = true;
                                gridBuilder.Items.AddRange(dashboardLayout.Items.Select(x => new DashboardItem(x)));
                                entity.DashboardLayout = gridBuilder;
                            }
                            item.Data = entity;
                            break;
                        }
                    case FormPreview formPreview:
                        {
                            var entity = new FormLayoutEntity(previewItem.TileCode);
                            if (layout != null)
                            {
                                var formLayout = JsonConvert.DeserializeObject<FormEditorModel>(layout.LayoutJson ?? "") ?? new(previewItem.TileCode);
                                entity.Controls = formLayout.Controls.Select(x => new FormEditorControlEntity(x)).ToList();
                            }
                            item.Data = entity;
                            break;
                        }
                    default:
                        throw new NotImplementedException();
                }
                layoutItems.Add(item);
            }

            return layoutItems;
        }

        public async Task<OperationResult> RemoveElement(TileCode tileCode, string itemId, EditorType type)
        {
            var layout = await m_TileContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
            if (layout == null)
                return new OperationResult(ResultCode.Error, $"Failed to find layout with tile code = {tileCode}");
            switch (type)
            {
                case EditorType.Filter:
                case EditorType.Form:
                    {
                        if (!layout.LayoutJson.TryParse<LayoutEditorModel>(out var layoutData))
                            return new OperationResult(ResultCode.Error, $"Failed to parse layout with id = {layout.Id}");
                        var index = layoutData.FormControls.FindIndex(x => ItemCodeHelper.GetItemCode(x) == itemId);
                        if (index >= 0)
                        {
                            layoutData.FormControls.RemoveAt(index);
                            layout.LayoutJson = JsonConvert.SerializeObject(layoutData);
                            m_TileContextService.Context.Layouts.Update(layout);
                            await m_TileContextService.Context.SaveChangesAsync();
                        }
                        break;
                    }
                case EditorType.Grid:
                    {
                        if (!layout.LayoutJson.TryParse<GridEditorModel>(out var layoutData))
                            return new OperationResult(ResultCode.Error, $"Failed to parse layout with id = {layout.Id}");
                        var index = layoutData.GridEntity.Layout.Columns.FindIndex(x => ItemCodeHelper.GetItemCode(x) == itemId);
                        if (index >= 0)
                        {
                            layoutData.GridEntity.Layout.Columns.RemoveAt(index);
                            layout.LayoutJson = JsonConvert.SerializeObject(layoutData);
                            m_TileContextService.Context.Layouts.Update(layout);
                            await m_TileContextService.Context.SaveChangesAsync();
                        }
                        break;
                    }
                default:
                    throw new NotImplementedException();
            }
            return new OperationResult(ResultCode.Success);
        }

        public async Task<FullScreenFormEditorModel> GetForm(TileCode tileCode, List<ControlPreviewModel>? controls, FormValueModel? formValueModel)
        {
            var result = new FullScreenFormEditorModel();
            var formLayout = await m_LayoutContextService.TryGetLayout<LayoutEditorModel>((int)tileCode) ?? new(tileCode);
            result.Controls = formLayout.FormControls.Select(x => new FormControl(x)).ToList();
            FillFormComponents(result.Components);
            return result;
        }

        private FormComponents FillFormComponents(FormComponents components)
        {
            components.Inputs = [.. EnumHelper.GetEnums<InputPresetCode>().Cast<int>()];
            components.Dropdowns = [.. EnumHelper.GetEnums<DropdownPresetCode>().Cast<int>()];
            components.Buttons = [.. EnumHelper.GetEnums<ButtonPresetCode>().Cast<int>()];
            components.Containers = [.. EnumHelper.GetEnums<ContainerPresetCode>().Cast<int>()];
            return components;
        }
    }
}
