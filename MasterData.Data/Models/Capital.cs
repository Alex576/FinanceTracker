using System;
using System.Collections.Generic;

namespace MasterData.Data.Models;

public partial class Capital
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Finance> Finances { get; set; } = new List<Finance>();
}
