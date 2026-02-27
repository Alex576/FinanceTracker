using FinanceTracker.Controllers.Api;
using FinanceTracker.Core.Builders;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Forms;
using FinanceTracker.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Services
{
    public class FormEditorService : IFormEditorService
    {
        private readonly IServiceProvider m_ServiceProvider;
        private readonly FinanceTrackerContext m_FinanceTrackerContext;

        public FormEditorService(IServiceProvider serviceProvider, FinanceTrackerContext financeTrackerContext)
        {
            m_ServiceProvider = serviceProvider;
            m_FinanceTrackerContext = financeTrackerContext;
        }

        public async Task<FormModel> GetForm(TileCode tileCode)
        {
            var formBuilder = ActivatorUtilities.CreateInstance<FormEditorLayoutBuilder>(m_ServiceProvider);
            var data = await m_FinanceTrackerContext.Layouts.FirstOrDefaultAsync(x => x.TileCode == (int)tileCode);
            return await formBuilder.GetFormLayout(tileCode, new(data ?? new() { TileCode = (int)tileCode }));
        }
    }
}
