using FinanceTracker.Core.Models;
using FinanceTracker.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService m_MenuService;

        public MenuController(IMenuService menuService)
        {
            m_MenuService = menuService;
        }

        [HttpGet("[action]")]
        public async Task<List<MenuItem>> GetMenuItems()
        {
            return await m_MenuService.GetMenuItems();
        }
    }
}
