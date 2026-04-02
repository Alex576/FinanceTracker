using FinanceTracker.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public static bool TryParseControlId(this string controlId, out TileItemCode tileItemCode, out int id)
        {
            var idParts = controlId.Split('_').ToList();
            if (idParts.Count != 2)
            {
                tileItemCode = default;
                id = default;
                return false;
            }

            tileItemCode = int.TryParse(idParts[0], out var tileItemCodeValue) ? (TileItemCode)tileItemCodeValue : default;
            id = int.TryParse(idParts[1], out var idValue) ? idValue : default;
            return true;
        }

        public static bool TryParse<T>(this string? json, out T data) where T : class
        {
            data = string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json);
            return data != default;
        }

        public static bool TryParse<T>(this JToken? value, out T parsedValue)
        {
            if (value == null)
            {
                parsedValue = default;
                return false;
            }
            if (value is T converted)
            {
                parsedValue = converted;
                return true;
            }
            try
            {
                parsedValue = JsonConvert.DeserializeObject<T>(value.ToString());
                return parsedValue != null;

            }
            catch (Exception)
            {
                parsedValue = default;
                return false;
            }
        }

        public static TileCode GetFilterTileCode(this ToolCode toolCode) => toolCode switch
        {
            ToolCode.Dashboard => throw new NotImplementedException(),
            ToolCode.Finances => TileCode.FinancesFilter,
            ToolCode.Settings => throw new NotImplementedException(),
            ToolCode.Roles => throw new NotImplementedException(),
            ToolCode.Users => throw new NotImplementedException(),
            ToolCode.Translation => throw new NotImplementedException(),
            ToolCode.Layout => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };

        public static TileCode GetGridTileCode(this ToolCode toolCode) => toolCode switch
        {
            ToolCode.Dashboard => throw new NotImplementedException(),
            ToolCode.Finances => TileCode.FinancesGrid,
            ToolCode.Settings => throw new NotImplementedException(),
            ToolCode.Roles => throw new NotImplementedException(),
            ToolCode.Users => throw new NotImplementedException(),
            ToolCode.Translation => throw new NotImplementedException(),
            ToolCode.Layout => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };

        public static TileCode GetDashboardTileCode(this ToolCode toolCode) => toolCode switch
        {
            ToolCode.Dashboard => throw new NotImplementedException(),
            ToolCode.Finances => TileCode.FinancesDashboard,
            ToolCode.Settings => throw new NotImplementedException(),
            ToolCode.Roles => throw new NotImplementedException(),
            ToolCode.Users => throw new NotImplementedException(),
            ToolCode.Translation => throw new NotImplementedException(),
            ToolCode.Layout => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };
    }
}
