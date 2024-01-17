using Microsoft.EntityFrameworkCore;
using Powtorka.Data;
using Powtorka.DTOs;

namespace Powtorka.Services
{
    public class MenuService : IMenuService
    {
        public Task<List<MenuItemDTO>> GetMenuItems(int IdRestauracji, PowtorkaDbContext context)
        {
            var res = context.MenuItems.Select(m => new MenuItemDTO
            {
                IdRes = m.Id,
                Name = m.Name,
                Price = m.Price
            }).Where(m => m.IdRes == IdRestauracji).ToListAsync();

            return res;
        }
    }
}
