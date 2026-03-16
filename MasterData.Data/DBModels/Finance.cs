using System;
using System.Collections.Generic;

namespace MasterData.Data.DBModels;

public partial class Finance
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CapitalId { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public DateTime? LastUpdate { get; set; }

    public int? LastModifiedUser { get; set; }

    public string? OptionsJson { get; set; }

    public virtual Capital Capital { get; set; } = null!;
}
