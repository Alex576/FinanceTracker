using FinanceTracker.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Builders.Layouts
{
    public class FormLayoutEditorBuilder : LayoutEditorBuilder
    {
        public FormLayoutEditorBuilder(TileContextService financeTrackerContext) : base(financeTrackerContext)
        {
        }
    }
}
