using FinanceTracker.Core.Models.Controls;
using FinanceTracker.Core.Models.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.FullScreenModels
{
    public class FullScreenUpdateModel
    {
        public List<FormControl> Controls { get; set; } = [];
        public FormModel OptionsForm { get; set; }

    }
}
