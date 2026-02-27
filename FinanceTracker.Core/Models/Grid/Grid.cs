using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.Grid
{
    public class Grid
    {
        public Layout Layout { get; set; }
        public List<List<object>> Rows { get; set; }
    }
}
