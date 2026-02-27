using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models
{
    public enum TileItemCode
    {
        Id = 1,
        Object = 2,
        Role = 3,
        UserName = 4,
        Fact = 5,
        Name = 6,
        Type = 7,
        State = 8,
        Tool = 9,
        Tile = 10,
    }

    public class TileItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
