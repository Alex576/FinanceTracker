using System;
using System.Collections.Generic;
using MasterData.Data.Models;

namespace MasterData.Data.DBModels;

public partial class FinanceType
{
    public int Type { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<FinanceItem> FinanceItems { get; set; } = new List<FinanceItem>();
}
