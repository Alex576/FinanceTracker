using FinanceTracker.Core.Models.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.FullScreenModels
{
    public class FullScreenFormEditorModel
    {
        public List<FormControl> Controls { get; set; } = [];
        //public TileCode TileCode { get; set; }
        public FormComponents Components { get; set; } = new();
    }

    public class FormComponents
    {
        public List<int> Inputs { get; set; } = [];
        public List<int> Dropdowns { get; set; } = [];
        public List<int> Buttons { get; set; } = [];
        public List<int> Containers { get; set; } = [];
    }
}
