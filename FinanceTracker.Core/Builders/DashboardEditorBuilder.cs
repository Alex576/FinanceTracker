using FinanceTracker.Core.Builders.Forms;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Dashboard;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.DashboardEditor;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using FinanceTracker.Core.Models.OperationResult;
using FinanceTracker.Core.Utils;
using FinanceTracker.Data.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Builders
{
    public class DashboardEditorBuilder : FormBuilder<DashboardItemOptions>
    {
        public DashboardEditorBuilder(FinanceContextServiceBase financeContextServiceBase, LayoutEditorModel layoutModel) : base(financeContextServiceBase, layoutModel)
        {
        }

        protected override List<Item> GetControlItems(FormControlData controlData, FormControl control, DashboardItemOptions data)
        {
            switch (controlData.TileItemCode)
            {
                default:
                    return base.GetControlItems(controlData, control, data);
            }
        }

        protected override object? GetControlValue(FormControlData controlData, DashboardItemOptions data)
        {
            object? value = controlData.TileItemCode switch
            {
                TileItemCode.Item => data.Data.Fields.FirstOrDefault()?.TileItemCode,
                TileItemCode.Name => data.Data.Name,
                _ => throw new NotImplementedException(),
            };
            return value;
        }

        protected override async Task<OperationResult> SaveLayout(TileCode tileCode, DashboardItemOptions data, FormValueModel formValueModel)
        {
            var layout = await m_FinanceContextServiceBase.Context.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
            if (layout == null)
            {
                layout = new() { TileCode = (int)tileCode };
            }
            var oldData = layout.LayoutJson.TryParse<DashboardEditorModel>(out var model) ? model : new(tileCode);
            //data.ColumnId = $"{(int)data.TileItemCode}";
            if (string.IsNullOrEmpty(data.Id))
                data.Id = Guid.NewGuid().ToString();
            var index = oldData.Items.FindIndex(x => x.Id == data.Id);

            if (index == -1)
                oldData.Items.Add(data);
            else
                oldData.Items[index] = data;
            layout.LayoutJson = JsonConvert.SerializeObject(oldData);

            if (layout.Id == 0)
                await m_FinanceContextServiceBase.Context.Layouts.AddAsync(layout);
            else
                m_FinanceContextServiceBase.Context.Layouts.Update(layout);

            var result = await m_FinanceContextServiceBase.Context.SaveChangesAsync();
            //var result = await SaveLayout(layout);
            return new OperationResult(result > 0 ? ResultCode.Success : ResultCode.Error);
        }

        protected override void UpdateData(DashboardItemOptions data, FormControlData controlData, FormControlValue controlValue)
        {
            switch (controlData.TileItemCode)
            {
                case TileItemCode.Item:
                    if (!controlValue.Value.TryParse(out TileItemCode itemCode))
                        break;
                    if (data.Data.Fields.Count == 0)
                        data.Data.Fields.Add(new DashboardItemField());
                    data.Data.Fields.First().TileItemCode = itemCode;
                    break;
                case TileItemCode.Name:
                    data.Data.Name = controlValue.Value?.ToString() ?? "";
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
