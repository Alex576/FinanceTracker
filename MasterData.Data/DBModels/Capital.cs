using System;
using System.Collections.Generic;

namespace MasterData.Data.DBModels;

public partial class Capital
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Finance> Finances { get; set; } = new List<Finance>();
}
