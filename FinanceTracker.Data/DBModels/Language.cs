using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.DBModels;

public partial class Language
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TranslationValue> TranslationValues { get; set; } = new List<TranslationValue>();
}
