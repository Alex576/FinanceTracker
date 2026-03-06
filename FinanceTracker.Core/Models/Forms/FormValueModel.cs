using FinanceTracker.Core.Utils;

namespace FinanceTracker.Core.Models.Forms
{
    public class FormValueModel
    {
        public List<FormControlValue> UpdatedControls { get; set; } = new();
        public TileCode TileCode { get; set; }
        public string? ItemId { get; set; }

        public bool TryGetControl(Func<FormControlValue, bool> condition, out FormControlValue control)
        {
            return UpdatedControls.TryGetValue(condition, out control);
        }

        public bool TryGetControlValue<T>(Func<FormControlValue, bool> condition, out T controlValue)
        {
            if (!TryGetControl(condition, out var control) || !control.Value.TryParse(out T value))
            {
                controlValue = default;
                return false;
            }
            controlValue = value;
            return true;
        }
    }
}
