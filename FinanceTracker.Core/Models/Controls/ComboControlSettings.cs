namespace FinanceTracker.Core.Models.Controls
{
    public class ComboControlSettings : ControlSettings
    {
        public List<Item> Items { get; set; }

        public bool AllowMultiselect { get; set; }
    }
}
