using FinanceTracker.Core.Models.Layout;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Services.Interfaces
{
    public interface ILayoutService
    {
        Task<LayoutEditor> GetLayoutEditor(Models.ToolCode toolCode);
        Task<LayoutManagementModel> GetLayoutManagement();
    }
}
