using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Utils
{
    public static class Extensions
    {
        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value) || !char.IsUpper(value[0]))
                return value;

            return char.ToLower(value[0]) + value.Substring(1);
        }
    }
}
