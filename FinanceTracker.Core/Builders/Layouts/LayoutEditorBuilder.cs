using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.ControlSettingModels;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Models.LayoutPreviews;
using FinanceTracker.Data.Models;
using FinanceTracker.Data.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinanceTracker.Core.Builders.Layouts
{
    public class LayoutEditorBuilder : BaseFormLayoutBuilder
    {
        private readonly FormValueModel m_FormValueModel;

        public LayoutEditorBuilder(FinanceContextService financeTrackerContext, FormValueModel? formValueModel = null) : base(financeTrackerContext)
        {
            m_FormValueModel = formValueModel ?? new();
        }

        public override async Task<LayoutPreview> GetLayoutAsync(ToolCode toolCode)
        {
            var layoutPreview = new LayoutPreview();
            var tileLayout = await m_FinanceTrackerContext.Context.Tiles.FirstOrDefaultAsync(x => x.ToolCode == (int)toolCode && x.Type == (int)TileTypeCode.Layout);
            if (tileLayout == null)
                return layoutPreview;


            return await GetLayoutAsync([new(tileLayout)]);
        }

        public Task<LayoutEditorModel> GetFormEditorLayout(TileCode tileCode)
        {

            var controls = new List<FormControlData>();
            controls.Add(GetControl("Name", TileItemCode.Name, ControlType.Input, [ControlState.Editable]));
            controls.Add(GetControl("Type", TileItemCode.Type, ControlType.Combo, [ControlState.Editable]));
            controls.Add(GetControl("State", TileItemCode.State, ControlType.Combo, [ControlState.Editable, ControlState.AllowMultiselect]));

            var itemControl = GetControl("Item", TileItemCode.Item, ControlType.Combo, [ControlState.Editable]);
            controls.Add(itemControl);
            var classControlDependsOnItem = GetControlDependence(TileItemCode.Item, DependencyType.Value, TileItemCode.Object);
            //if (m_FormValueModel.TryGetControlValue<int>(x => x.ControlId == itemControl.Id, out var controlValue) && controlValue == (int)TileItemCode.Object)
            controls.Add(GetControl("Class", TileItemCode.Class, ControlType.Combo, [ControlState.Hidden, ControlState.Editable, ControlState.AllowMultiselect], classControlDependsOnItem));

            return Task.FromResult(new LayoutEditorModel() { FormControls = controls });
        }

        private FormControlData GetControl(string name, TileItemCode tileItemCode, ControlType type, List<ControlState> states, ControlDependence? dependence = null)
        {
            var control = new FormControlData() { Id = GetControlId(tileItemCode), Name = name, TileItemCode = tileItemCode, Type = type, Dependence = dependence };
            control.ControlStates.AddRange(states);
            return control;
        }

        private ControlDependence GetControlDependence(TileItemCode tileItemCode, DependencyType type, object value)//todo implement dependency logic!!
        {
            var dependence = new ControlDependence();
            //dependence.Add(new DependenceCriteria() { Type = type, })

            return dependence;
        }


        //public virtual async Task<LayoutEditorModel> GetFormData(TileCode tileCode)
        //{
        //    var layoutEntity = await m_FinanceTrackerContext.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
        //    if (layoutEntity == null || string.IsNullOrEmpty(layoutEntity.LayoutJson))
        //        throw new Exception($"Failed to find layout, tile code = {tileCode}");
        //    var layout = JsonConvert.DeserializeObject<LayoutEditorModel>(layoutEntity.LayoutJson) ?? new();
        //    return layout;
        //}

        public override async Task<LayoutPreview> GetLayoutAsync(List<Tile> layoutTiles)
        {
            var layoutPreview = new LayoutPreview();
            foreach (var tile in layoutTiles.Where(x => x.Type == TileTypeCode.Layout))
            {
                var layoutItems = await m_FinanceTrackerContext.GetTilesChildren([(int)tile.TileCode]).Select(x => new Tile(x)).ToListAsync();
                //var layoutItems = await financeTrackerContext.Tiles.Where(x => x.TileCode != (int)tile.TileCode && x.HierarchyPath.IsDescendantOf(tile.Hierarchy)).Select(x => new Tile(x)).ToListAsync();
                foreach (var layoutItem in layoutItems.OrderBy(x => x.Order ?? 0))
                {
                    switch (layoutItem.Type)
                    {
                        case TileTypeCode.Dashboard:
                            layoutPreview.Previews.Add(new DashboardPreview(layoutItem.TileCode));
                            break;
                        case TileTypeCode.Grid:
                            layoutPreview.Previews.Add(new GridPreview(layoutItem.TileCode));
                            break;
                        case TileTypeCode.Form:
                            break;
                        case TileTypeCode.Filter:
                            layoutPreview.Previews.Add(new FilterPreview(layoutItem.TileCode));
                            break;
                        case TileTypeCode.Layout:
                            break;
                        default:
                            break;
                    }
                }
            }
            return layoutPreview;
        }
    }
}
