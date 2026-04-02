using System;
using System.Collections.Generic;
using MasterData.Data.Models;

namespace MasterData.Data.DBModels;

public partial class ObjectEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int ClassCode { get; set; }

    public virtual ClassEntity ClassCodeNavigation { get; set; } = null!;
}
