using System;
using System.Collections.Generic;
using MasterData.Data.Models;

namespace MasterData.Data.DBModels;

public partial class Capital
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }
}
