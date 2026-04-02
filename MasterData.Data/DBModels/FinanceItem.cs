using System;
using System.Collections.Generic;
using MasterData.Data.Models;

namespace MasterData.Data.DBModels;

public partial class FinanceItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public DateTime? LastUpdate { get; set; }

    public int? LastModifiedUser { get; set; }

    public FinanceOptionsData? OptionsJson { get; set; }

    public int? ParentFinanceId { get; set; }

    public int FinanceType { get; set; }

    public virtual FinanceType FinanceTypeNavigation { get; set; } = null!;

    public virtual ICollection<FinanceItem> InverseParentFinance { get; set; } = new List<FinanceItem>();

    public virtual FinanceItem? ParentFinance { get; set; }
}
