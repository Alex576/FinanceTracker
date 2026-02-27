using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.Controls
{
    public class ComboControlSettings : ControlSettings
    {
        public List<Item> Items { get; set; }

        public bool AllowMultiselect { get; set; }
    }
}
