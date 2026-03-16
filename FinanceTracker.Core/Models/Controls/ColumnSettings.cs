using FinanceTracker.Core.Models.Grid;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.Controls
{
    public class ColumnSettings : ControlSettings
    {
        public int? Width { get; set; }
        public PinPosition Pinned { get; set; }
        public bool LockPinned { get; set; }
        public bool AutoHeight { get; set; }
        public bool WrapText { get; set; }
        public bool Sortable { get; set; }
        public int? MaxWidth { get; set; }
        public bool Resizable { get; set; }
    }
}
