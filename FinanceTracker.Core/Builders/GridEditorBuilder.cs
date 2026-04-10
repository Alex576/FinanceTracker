using FinanceTracker.Core.Builders.Forms;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.ControlSettingModels;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Models.LayoutEntities;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinanceTracker.Core.Builders
{
    public class GridEditorBuilder : FormBuilder<ColumnEntity>
    {
        public GridEditorBuilder(FinanceContextServiceBase financeContextServiceBase, LayoutEditorModel layoutModel) : base(financeContextServiceBase, layoutModel)
        {
        }

        protected override List<Item> GetControlItems(FormControlData controlData, FormControl control, ColumnEntity data)
        {
            switch (controlData.TileItemCode)
            {
                //case TileItemCode.Item:
                //    return EnumHelper.GetEnums<TileItemCode>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                //case TileItemCode.Class:
                //    return EnumHelper.GetEnums<ClassCode>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                //case TileItemCode.DataType:
                //    return EnumHelper.GetEnums<DataType>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                //case TileItemCode.Type:
                //    return EnumHelper.GetEnums<ControlType>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                case TileItemCode.ColumnDataType:
                    return EnumHelper.GetEnums<ColumnDataType>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                //case TileItemCode.State:
                //    return EnumHelper.GetEnums<ControlState>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                default:
                    return base.GetControlItems(controlData, control, data) ;
            }
        }

        protected override object? GetControlValue(FormControlData controlData, ColumnEntity data)
        {
            object? value = controlData.TileItemCode switch
            {
                TileItemCode.Item => data.TileItemCode,
                TileItemCode.Class => data.ControlMasterData.ClassCodes,
                TileItemCode.Name => data.Name,
                TileItemCode.ColumnDataType => data.ColumnDataType,
                TileItemCode.State => data.ControlStates,
                TileItemCode.Fact => data.ControlMasterData.FactModel.Name,
                TileItemCode.DataType => data.ControlMasterData.FactModel.DataType,
                _ => null,
            };
            return value;
        }

        protected override async Task<OperationResult> SaveLayout(TileCode tileCode, ColumnEntity data, FormValueModel formValueModel)
        {
            var layout = await m_FinanceContextServiceBase.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
            if (layout == null)
            {
                layout = new() { TileCode = (int)tileCode };
            }
            var oldData = layout.LayoutJson.TryParse<GridEditorModel>(out var model) ? model : new(tileCode);
            //data.ColumnId = $"{(int)data.TileItemCode}";
            var index = oldData.GridEntity.Layout.Columns.FindIndex(x => x.TileItemCode == data.TileItemCode);

            if (index == -1)
                oldData.GridEntity.Layout.Columns.Add(data);
            else
                oldData.GridEntity.Layout.Columns[index] = data;
            layout.LayoutJson = JsonConvert.SerializeObject(oldData);

            if (layout.Id == 0)
                await m_FinanceContextServiceBase.Context.Layouts.AddAsync(layout);
            else
                m_FinanceContextServiceBase.Context.Layouts.Update(layout);

            var result = await m_FinanceContextServiceBase.Context.SaveChangesAsync();
            //var result = await SaveLayout(layout);
            return new OperationResult(result > 0 ? ResultCode.Success : ResultCode.Error);
        }

        protected override void UpdateData(ColumnEntity data, FormControlData controlData, FormControlValue controlValue)
        {
            switch (controlData.TileItemCode)
            {
                case TileItemCode.Item when controlValue.Value.TryParse(out TileItemCode itemCode):
                    data.TileItemCode = itemCode;
                    break;
                case TileItemCode.Class when controlValue.Value.TryParse(out List<int> classes):
                    data.ControlMasterData.ClassCodes = classes;
                    break;
                case TileItemCode.Name:
                    data.Name = controlValue.Value?.ToString() ?? "";
                    break;
                case TileItemCode.Fact when !string.IsNullOrEmpty(controlValue.Value?.ToString()):
                    data.ControlMasterData.FactModel.Name = controlValue.Value.ToString();
                    break;
                case TileItemCode.DataType when controlValue.Value.TryParse(out DataType dataType):
                    data.ControlMasterData.FactModel.DataType = dataType;
                    break;
                case TileItemCode.ColumnDataType when controlValue.Value.TryParse(out ColumnDataType columnDataType):
                    data.ColumnDataType = columnDataType;
                    break;
                case TileItemCode.State when controlValue.Value.TryParse(out List<int> states):
                    data.ControlStates = states.Cast<ControlState>().ToList();
                    break;
                default:
                    break;
            }
        }
    }
}
