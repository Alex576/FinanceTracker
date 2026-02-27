using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.ControlDataSettings;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.DBContext;
using FinanceTracker.Data.DBModels;
using MasterData.Data.DBContext;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FinanceTracker.Core.Builders
{
    public class FormEditorLayoutBuilder : LayoutBuilder<LayoutEditorModel<FormLayoutData>>
    {
        private int _index = 0;
        private readonly MasterDataContext m_MasterDataContext;

        public FormEditorLayoutBuilder(FinanceTrackerContext financeTrackerContext, MasterDataContext masterDataContext) : base(financeTrackerContext)
        {
            m_MasterDataContext = masterDataContext;
        }

        protected override Task<FormLayoutData> GetFormData(TileCode tileCode)
        {
            var controls = new List<FormControlData>();
            controls.Add(GetControl(_index++, "Name", TileItemCode.Name, ControlType.Input, [ControlState.Editable]));
            controls.Add(GetControl(_index++, "Type", TileItemCode.Type, ControlType.Combo, [ControlState.Editable]));
            controls.Add(GetControl(_index++, "State", TileItemCode.State, ControlType.Combo, [ControlState.Editable, ControlState.AllowMultiselect]));

            return Task.FromResult(new FormLayoutData() { FormControls = controls });
        }

        private FormControlData GetControl(int id, string name, TileItemCode tileItemCode, ControlType type, List<ControlState> states)
        {
            var control = new FormControlData() { Id = $"{id}", Name = name, TileItemCode = tileItemCode, Type = type };
            control.ControlDataSettings.ControlStates.AddRange(states);
            return control;
        }

        protected override List<Item> GetControlItems(FormControlData controlData, FormControl control)
        {
            switch (controlData.TileItemCode)
            {
                case TileItemCode.Object:
                    return m_MasterDataContext.ObjectEntities.Select(x => new Item() { Id = x.Id, Name = x.Name }).ToList();//todo rewrite to async or storage
                case TileItemCode.Type:
                    return EnumHelper.GetEnums<ControlType>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                case TileItemCode.State:
                    return EnumHelper.GetEnums<ControlState>().Select(x => new Item() { Id = (int)x, Name = x.ToString() }).ToList();
                default:
                    return null;
            }
        }
        protected override object? GetControlValue(FormControlData controlData, FormControl control, LayoutEditorModel<FormLayoutData> data)
        {
            return controlData.Value;
            return controlData.TileItemCode switch
            {
                //TileItemCode.Id => throw new NotImplementedException(),
                //TileItemCode.Object => throw new NotImplementedException(),
                //TileItemCode.Role => throw new NotImplementedException(),
                //TileItemCode.UserName => throw new NotImplementedException(),
                //TileItemCode.Fact => throw new NotImplementedException(),
                TileItemCode.Name => controlData.Name,
                TileItemCode.Type => controlData.Type,
                TileItemCode.State => controlData.ControlDataSettings.ControlStates,
                _ => null,
            };
        }

        protected override void UpdateData(LayoutEditorModel<FormLayoutData> data, FormControlData controlData, FormControlValue controlValue)
        {
            switch (controlData.TileItemCode)
            {
                //case TileItemCode.Object when controlData.ControlDataSettings is ObjectControlDataSettings objectControlData:
                //    return objectControlData.ObjCodes
                //case TileItemCode.Role:
                //    break;
                //case TileItemCode.UserName:
                //    break;
                //case TileItemCode.Fact:
                //    break;
                case TileItemCode.Name when controlValue.Value is string nameString:
                    controlData.Value = nameString;
                    break;
                case TileItemCode.Type:
                    controlData.Value = (ControlType)controlValue.Value;
                    break;
                case TileItemCode.State when controlValue.Value is List<int> states:
                    controlData.Value = states.Cast<ControlState>().ToList();
                    break;
                default:
                    break;
            }
        }

        public override async Task<OperationResult> SaveForm(TileCode tileCode, LayoutEditorModel<FormLayoutData> data, FormValueModel formValueModel)
        {
            var result = await SaveLayout(data.GetLayout());
            return new OperationResult(result > 0 ? ResultCode.Success : ResultCode.Error);
        }

        protected override object? GetDefaultValue(FormControlData controlData, FormControl control)
        {
            return null;
        }
    }
}
