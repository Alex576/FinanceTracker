using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.Controls
{
    [Obsolete]
    public class FormSelectControl : FormControl
    {
        public List<Item> Items { get; set; }
    }
}
