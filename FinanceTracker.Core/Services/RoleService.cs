using FinanceTracker.Core.Builders.Grids;
using FinanceTracker.Core.Models;
using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Security.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IServiceProvider m_ServiceProvider;
        private readonly SecurityContext m_SecurityContext;

        public RoleService(IServiceProvider serviceProvider, SecurityContext securityContext)
        {
            m_ServiceProvider = serviceProvider;
            m_SecurityContext = securityContext;
        }

        public async Task<Grid> GetGridLayout()
        {
            var gridBuilder = ActivatorUtilities.CreateInstance<RolesGridBuilder>(m_ServiceProvider);
            var roles = await m_SecurityContext.Roles.ToListAsync();

            return gridBuilder.GetLayout(roles.Select(r => new Role() { Id = r.Id, Name = r.Name }).ToList());

        }
    }
}
