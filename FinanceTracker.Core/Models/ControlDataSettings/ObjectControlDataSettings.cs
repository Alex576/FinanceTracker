using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.ControlDataSettings
{
    public class ObjectControlDataSettings : ControlDataSettings
    {
        public List<int> ObjCodes { get; set; } = new();

    }
}
