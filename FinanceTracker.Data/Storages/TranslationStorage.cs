using FinanceTracker.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Data.Storages
{
    public class TranslationStorage
    {
        private readonly Dictionary<string, string> _allTranslations = new Dictionary<string, string>();
        public IReadOnlyDictionary<string, string> AllTranslations { get { return _allTranslations; } }

        public async Task LoadAsync(FinanceTrackerContext context)
        {
            var allTranslationKeys = await context.TranslationKeys.ToDictionaryAsync(x => x.Id, x => x);
            var allTranslationValues = await context.TranslationValues.ToListAsync();
            foreach (var value in allTranslationValues)
            {
                if (allTranslationKeys.TryGetValue(value.KeyId, out var translation))
                {
                    var keyParts = new Stack<string>();
                    keyParts.Push(translation.KeyName);
                    var parentId = translation.ParentId;
                    while (parentId.HasValue)
                    {
                        if (allTranslationKeys.TryGetValue(parentId.Value, out var parent))
                            keyParts.Push(parent.KeyName);

                        parentId = parent?.ParentId;
                    }
                    _allTranslations.Add(string.Join(".", keyParts), value.Value);
                }

            }
        }

    }
}
