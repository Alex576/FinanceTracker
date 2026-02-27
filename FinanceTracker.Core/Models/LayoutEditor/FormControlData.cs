using FinanceTracker.Core.Models.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.ControlDataSettings
{
    /// <summary>
    /// Used in layout editor forms, should to store in database
    /// </summary>
    public class FormControlData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ControlType Type { get; set; }
        public object? Value { get; set; }

        public TileItemCode TileItemCode { get; set; }
        public ControlMasterData ControlMasterData { get; set; } = new();

        private ControlDataSettings _controlDataSettings;

        public ControlDataSettings ControlDataSettings
        {
            get
            {
                _controlDataSettings ??= GetControlSettings();
                return _controlDataSettings;
            }

        }

        private ControlDataSettings GetControlSettings() => TileItemCode switch
        {
            TileItemCode.Object => new ObjectControlDataSettings(),
            TileItemCode.Fact => new FactControlDataSettings(),
            _ => new InputControlDataSettings(),
        };
    }
}
