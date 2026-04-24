using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.DBModels;

public partial class TranslationKey
{
    public int Id { get; set; }

    public string KeyName { get; set; } = null!;

    public int? ParentId { get; set; }

    public virtual ICollection<TranslationKey> InverseParent { get; set; } = new List<TranslationKey>();

    public virtual TranslationKey? Parent { get; set; }

    public virtual ICollection<TranslationValue> TranslationValues { get; set; } = new List<TranslationValue>();
}
