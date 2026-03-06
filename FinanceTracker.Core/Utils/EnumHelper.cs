namespace FinanceTracker.Core.Utils
{
    public static class EnumHelper
    {
        public static List<TEnum> GetEnums<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetValues<TEnum>().ToList();
        }
    }
}
