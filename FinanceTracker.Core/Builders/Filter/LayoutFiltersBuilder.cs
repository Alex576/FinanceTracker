using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.LayoutEditor;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Builders.Filter
{
    public class LayoutFiltersBuilder : FilterBuilder<FiltersEditorModel>
    {
        public LayoutFiltersBuilder(List<FormControlData> controlDatas) : base(controlDatas)
        {
        }

        public FormControl GetFilterControl(string name, List<Item> items, string id, TileItemCode tileItemCode)
        {
            var settings = new ComboControlSettings()
            {
                AllowMultiselect = false,
                Editable = true,
                Items = items
            };
            var toolFilter = new FormControl()
            {
                Id = id,
                Name = name,
                TileItemCode = tileItemCode,
                Type = ControlType.Combo,
                Settings = settings,
                Value = GetComboDefaultValue(settings),
            };
            return toolFilter;
        }

        protected override List<Item> GetControlItems(FormControlData controlData, FormControl control, FiltersEditorModel data)
        {
            throw new NotImplementedException();
        }

        protected override object? GetControlValue(FormControlData controlData, FiltersEditorModel data)
        {
            throw new NotImplementedException();
        }
    }
}
