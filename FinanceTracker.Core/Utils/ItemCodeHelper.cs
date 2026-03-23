using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.LayoutEditor.EditorModels;
using FinanceTracker.Core.Models.LayoutEditor.GridEditor;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Utils
{
    public static class ItemCodeHelper
    {
        private const int AttributeStartCode = 1000000;
        private const int FactStartCode = 10000000;
        private static int GetStartCode(TileItemCode tileItemCode) => tileItemCode switch
        {
            TileItemCode.Attribute => AttributeStartCode,
            TileItemCode.Fact => FactStartCode,
            _ => 0,
        };

        private static int ParseStartCode(int startCode)
        {
            if (TryParse(startCode, FactStartCode, out var value) || TryParse(startCode, AttributeStartCode, out value))
                return value;

            return startCode;

            bool TryParse(int code, int startCode, out int value)
            {
                value = code & startCode;
                return value > 0;
            }
        }

        public static string GetItemCode(TileItemCode tileItemCode, int additionalCode = 0)
        {
            var startCode = GetStartCode(tileItemCode) + (int)tileItemCode;
            return $"{startCode}_{additionalCode}";
        }

        public static bool TryParseItemCode(string itemCode, out TileItemCode tileItemCode, out int additionalCode)
        {
            var parts = itemCode.Split('_').ToList();
            if (parts.Count != 2)
            {
                tileItemCode = default;
                additionalCode = default;
                return false;
            }
            tileItemCode = int.TryParse(parts[0], out var tileItemCodeValue) ? (TileItemCode)ParseStartCode(tileItemCodeValue) : default;
            additionalCode = int.TryParse(parts[1], out var idValue) ? idValue : default;
            return true;
        }

        public static string GetItemCode(ColumnEntity data) => GetItemCode(data.TileItemCode);
        public static string GetItemCode(FormControlData data) => GetItemCode(data.TileItemCode);
    }
}
