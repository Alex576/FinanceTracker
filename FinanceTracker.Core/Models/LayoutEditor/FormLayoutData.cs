using FinanceTracker.Core.Models.ControlDataSettings;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.LayoutEditor
{
    /// <summary>
    /// used in layout editor forms
    /// </summary>
    public class FormLayoutData
    {
        public List<FormControlData> FormControls { get; set; } = new();
    }
}
