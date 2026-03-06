namespace FinanceTracker.Core.Models.ControlSettingModels
{
    public class ObjectControlDataSettings : ControlDataSettings
    {
        public List<int> ObjCodes { get; set; } = new();
        public List<int> ClassCodes { get; set; } = new();
    }
}
