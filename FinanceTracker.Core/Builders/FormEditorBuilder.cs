using FinanceTracker.Core.Builders.Forms;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.ControlSettingModels;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinanceTracker.Core.Builders
{
    public class FormEditorBuilder : FormBuilder<FormControlData>
    {

        public FormEditorBuilder(FinanceContextServiceBase financeContextServiceBase, LayoutEditorModel layoutModel) : base(financeContextServiceBase, layoutModel) { }

        protected override List<Item> GetControlItems(FormControlData controlData, FormControl control, FormControlData data)
        {
            switch (controlData.TileItemCode)
            {
                case TileItemCode.Item:
                    return EnumHelper.GetEnums<TileItemCode>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                case TileItemCode.Class:
                    return EnumHelper.GetEnums<ClassCode>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                case TileItemCode.DataType:
                    return EnumHelper.GetEnums<DataType>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                case TileItemCode.Type:
                    return EnumHelper.GetEnums<ControlType>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                case TileItemCode.State:
                    return EnumHelper.GetEnums<ControlState>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                default:
                    return [];
            }
        }

        protected override object? GetControlValue(FormControlData controlData, FormControlData data)
        {
            //return controlData.Value;
            object? value = controlData.TileItemCode switch
            {
                TileItemCode.Item => (int)data.TileItemCode,
                TileItemCode.Class => data.ControlMasterData.ClassCodes,
                TileItemCode.Name => data.Name,
                TileItemCode.Type => (int)data.Type,
                TileItemCode.State => data.ControlStates.Cast<int>().ToList(),
                TileItemCode.Fact => data.ControlMasterData.FactModel.Name,
                TileItemCode.DataType => (int)data.ControlMasterData.FactModel.DataType,
                _ => null,
            };
            return value;
        }

        protected override void UpdateData(FormControlData data, FormControlData controlData, FormControlValue controlValue)
        {
            switch (controlData.TileItemCode)
            {
                case TileItemCode.Item when controlValue.Value.TryParse(out int itemCode):
                    data.TileItemCode = (TileItemCode)itemCode;
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
                case TileItemCode.Type when controlValue.Value.TryParse(out ControlType controlType):
                    data.Type = controlType;
                    break;
                case TileItemCode.State when controlValue.Value.TryParse(out List<int> states):
                    data.ControlStates = states.Cast<ControlState>().ToList();
                    break;
                default:
                    break;
            }
        }

        protected override async Task<OperationResult> SaveLayout(TileCode tileCode, FormControlData data, FormValueModel formValueModel)
        {
            var layout = await m_FinanceContextServiceBase.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
            if (layout == null)
            {
                layout = new() { TileCode = (int)tileCode };
            }
            var oldData = layout.LayoutJson.TryParse<LayoutEditorModel>(out var model) ? model : new(tileCode);
            //data.Id = $"{(int)data.TileItemCode}_{data.Name}";
            var index = oldData.FormControls.FindIndex(x => x.TileItemCode == data.TileItemCode);

            if (index == -1)
                oldData.FormControls.Add(data);
            else
                oldData.FormControls[index] = data;
            layout.LayoutJson = JsonConvert.SerializeObject(oldData);

            if (layout.Id == 0)
                await m_FinanceContextServiceBase.Context.Layouts.AddAsync(layout);
            else
                m_FinanceContextServiceBase.Context.Layouts.Update(layout);

            var result = await m_FinanceContextServiceBase.Context.SaveChangesAsync();
            //var result = await SaveLayout(layout);
            return new OperationResult(result > 0 ? ResultCode.Success : ResultCode.Error);
        }

        protected override JToken? GetDefaultValue(FormControl control)
        {
            return null;
        }
    }
}
