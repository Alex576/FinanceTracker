namespace FinanceTracker.Core.Utils
{
    public static class ListExtensions
    {
        public static bool TryGetValue<T>(this IEnumerable<T> enumerable, Func<T, bool> condition, out T value)
        {
            value = enumerable.FirstOrDefault(condition);
            return value != null;
        }

        public static bool TryGetSingleValue<T>(this IEnumerable<T> enumerable, Func<T, bool> condition, out T value)
        {
            var items = enumerable.Where(condition).ToList();
            value = items.Count == 0 || items.Count > 1 ? default : items.First();
            return value != null;
        }
    }
}
