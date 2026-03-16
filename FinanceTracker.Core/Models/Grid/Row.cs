using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.Grid
{
    public class Row
    {
        public List<JToken?> Data { get; set; } = new();
    }
}
