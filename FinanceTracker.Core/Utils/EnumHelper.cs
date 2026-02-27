using FinanceTracker.Core.Models.ControlDataSettings;
using FinanceTracker.Core.Models.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Utils
{
    public static class EnumHelper
    {
        public static List<TEnum> GetEnums<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetValues<TEnum>().ToList();
        }
    }
}
