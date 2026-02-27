using FinanceTracker.Core.Models.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Builders.Forms
{
    public abstract class BaseFormBuilder
    {
        public abstract FormModel GetForm();
        public abstract FormModel UpdateForm();
    }
}
