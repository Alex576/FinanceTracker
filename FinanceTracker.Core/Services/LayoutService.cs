using FinanceTracker.Core.Builders.Filter;
using FinanceTracker.Core.Builders.Layouts;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEntities;
using FinanceTracker.Core.Models.LayoutPreviews;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.Models;
using FinanceTracker.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FinanceTracker.Core.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly IServiceProvider m_ServiceProvider;
        private readonly FinanceContextService m_FinanceContextService;

        public LayoutService(IServiceProvider serviceProvider, FinanceContextService financeContextService)
        {
            m_ServiceProvider = serviceProvider;
            m_FinanceContextService = financeContextService;
        }

        public async Task<LayoutManagementModel> GetLayoutManagement()
        {
            var tools = await m_FinanceContextService.Context.Tools.Where(x => x.Id != (int)ToolCode.Settings).ToListAsync();
            var filterBuilder = ActivatorUtilities.CreateInstance<FilterBuilder>(m_ServiceProvider);
            var model = new LayoutManagementModel();
            var toolItems = tools.Select(x => new Item() { Id = x.Id, Name = x.Name }).ToList();
            model.ToolFilter = GetFilterControl(filterBuilder, "Tools", toolItems, Constants.ToolFilterId, TileItemCode.Tool);

            //model.ToolFilter = filterBuilder.GetFilterControl(toolItems, "Tool", new ComboControlSettings() { AllowMultiselect = false }, Constants.ToolFilterId);

            var tiles = model.ToolFilter.Value.TryParse(out int toolValue) ? await GetTileUnderTool(toolValue) : [];

            var tileItems = tiles.Select(x => new Item() { Id = (int)x.TileCode, Name = x.Name }).ToList();
            //model.TileFilter = filterBuilder.GetFilterControl(controlItems, "Tiles", new ComboControlSettings() { AllowMultiselect = false }, Constants.TileFilterId);
            model.TileFilter = GetFilterControl(filterBuilder, "Tiles", tileItems, Constants.TileFilterId, TileItemCode.Tile);

            return model;
        }

        private FormControl GetFilterControl(FilterBuilder filterBuilder, string name, List<Item> items, string id, TileItemCode tileItemCode)
        {
            var toolFilter = new FormControl()
            {
                Id = id,
                Name = name,
                TileItemCode = tileItemCode,
                Type = ControlType.Combo,
                Settings = new ComboControlSettings()
                {
                    AllowMultiselect = false,
                    Editable = true,
                    Items = items
                },
            };
            toolFilter.Value = filterBuilder.FillEmptyValue(toolFilter.Settings);
            return toolFilter;
        }

        private async Task<List<Tile>> GetTileUnderTool(int toolValue)
        {
            return await m_FinanceContextService.Context.Tiles.Where(x => x.ToolCode == toolValue).Select(x => new Tile(x)).ToListAsync();
        }

        public async Task<LayoutEditor> GetLayoutEditor(ToolCode toolCode)
        {
            var layoutEditor = new LayoutEditor();

            var filterBuilder = ActivatorUtilities.CreateInstance<FilterBuilder>(m_ServiceProvider);
            var tiles = await GetTileUnderTool((int)toolCode);
            var controlItems = tiles.Select(x => new Item() { Id = (int)x.TileCode, Name = x.Name }).ToList();
            //layoutEditor.TileFilter = filterBuilder.GetFilterControl(controlItems, "Tiles", new ComboControlSettings() { AllowMultiselect = false }, Constants.TileFilterId);
            layoutEditor.TileFilter = GetFilterControl(filterBuilder, "Tiles", controlItems, Constants.TileFilterId, TileItemCode.Tile);// filterBuilder.GetFilterControl(controlItems, "Tiles", new ComboControlSettings() { AllowMultiselect = false }, );

            var layoutBuilder = new LayoutEditorBuilder(m_FinanceContextService);
            //var layoutTile = tiles.FirstOrDefault(t => t.Type == (int)TileTypeCode.Layout);
            //if (layoutTile != null)

            layoutEditor.LayoutItems = await GetLayoutEditorItems(await layoutBuilder.GetLayoutAsync(tiles), filterBuilder);
            return layoutEditor;
        }

        private async Task<List<LayoutEntity>> GetLayoutEditorItems(LayoutPreview layoutPreview, FilterBuilder filterBuilder)
        {
            var layoutItems = new List<LayoutEntity>();
            foreach (var previewItem in layoutPreview.Previews)
            {
                var layout = await m_FinanceContextService.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)previewItem.TileCode);
                //if (layout == null)
                //    continue;
                if (previewItem is FilterPreview filterPreview)
                {
                    var item = new LayoutEntity() { TileCode = previewItem.TileCode };
                    var entity = new FilterLayoutEntity(previewItem.TileCode);
                    if (layout != null)
                    {
                        var filters = JsonConvert.DeserializeObject<LayoutEditorModel>(layout.LayoutJson ?? "") ?? new();
                        entity.Filters = filters.FormControls.Select(x => new FormControl(x)).ToList() ;
                    }
                    item.Data = entity;
                    layoutItems.Add(item);
                    //layoutItems.Add(new(LayoutBlockNames.FILTER_BLOCK, filterLayout));

                }
                else if (previewItem is GridPreview gridPreview)
                {
                    var item = new LayoutEntity() { TileCode = previewItem.TileCode };
                    var entity = new GridLayoutEntity(previewItem.TileCode);
                    //if (layout != null)
                    //    item.Data = JsonConvert.DeserializeObject<GridLayoutEntity>(layout.LayoutJson ?? "") ?? new();
                    item.Data = entity;
                    layoutItems.Add(item);
                    //layoutItems.Add(new(LayoutBlockNames.GRID_BLOCK, gridLayout));
                }
            }

            return layoutItems;
        }
    }
}
