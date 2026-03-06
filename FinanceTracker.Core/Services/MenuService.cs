using FinanceTracker.Core.Models;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Core.Services
{
    public class MenuService : IMenuService
    {
        private readonly FinanceTrackerContext m_TrackerContext;

        public MenuService(FinanceTrackerContext trackerContext)
        {
            m_TrackerContext = trackerContext;
        }

        public async Task<List<MenuItem>> GetMenuItems()
        {
            var menuItems = (await m_TrackerContext.MenuItems.ToListAsync()).Select(x =>
            {
                return new MenuItem()
                {
                    Id = (MenuCode)x.Id,
                    Name = x.Name,
                    ParentId = x.ParentMenuCode,
                    ToolCode = (ToolCode)x.ToolCode,
                };
            }).ToList();
            return menuItems;
        }
    }
}
