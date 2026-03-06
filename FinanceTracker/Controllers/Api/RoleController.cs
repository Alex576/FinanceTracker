using FinanceTracker.Core.Models.Grid;
using FinanceTracker.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService m_RoleService;

        public RoleController(IRoleService roleService)
        {
            m_RoleService = roleService;
        }

        [HttpGet("[action]")]
        public async Task<Grid> GetAllRolesGrid()
        {
            return await m_RoleService.GetGridLayout();
        }
    }
}
