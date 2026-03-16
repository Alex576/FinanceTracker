using System;
using System.Collections.Generic;

namespace MasterData.Data.DBModels;

public partial class ClassEntity
{
    public int ClassCode { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ObjectEntity> ObjectEntities { get; set; } = new List<ObjectEntity>();
}
