using FinanceTracker.Core.Models.Controls;

namespace FinanceTracker.Core.Models.Forms
{
    /// <summary>
    /// Used in real forms
    /// </summary>
    public class FormModel
    {
        public List<FormControl> Controls { get; set; } = new();
    }
}