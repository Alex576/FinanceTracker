using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.LayoutEditor
{
    public class LayoutEditorModel<TData> where TData : class
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? LayoutJson { get; set; }

        private TData _layout;

        public TData Layout
        {
            get
            {
                _layout ??= JsonConvert.DeserializeObject<TData>(LayoutJson ?? "") ?? Activator.CreateInstance<TData>();
                return _layout;
            }

        }

        public int TileCode { get; set; }

        public LayoutEditorModel(int id, string name, string? layoutJson, int tileCode)
        {
            Id = id;
            Name = name;
            LayoutJson = layoutJson;
            TileCode = tileCode;
        }

        public LayoutEditorModel(FinanceTracker.Data.DBModels.Layout layout) : this(layout.Id, layout.Name, layout.LayoutJson, layout.TileCode) { }

        public FinanceTracker.Data.DBModels.Layout GetLayout() => new()
        {
            Id = Id,
            Name = Name,
            LayoutJson = LayoutJson,
            TileCode = TileCode
        };
    }
}
