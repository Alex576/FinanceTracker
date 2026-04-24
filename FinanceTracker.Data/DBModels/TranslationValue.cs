using System;
using System.Collections.Generic;

namespace FinanceTracker.Data.DBModels;

public partial class TranslationValue
{
    public int Id { get; set; }

    public int KeyId { get; set; }

    public int Language { get; set; }

    public string Value { get; set; } = null!;

    public virtual TranslationKey Key { get; set; } = null!;

    public virtual Language LanguageNavigation { get; set; } = null!;
}
