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
        public MenuItemDTO AddMenuItem(int IdRestauracji, PowtorkaDbContext context, MenuItemDTO menuItemDTO)
        {
            context.MenuItems.AddAsync(new Models.MenuItem() { 
                Name = menuItemDTO.Name,
                Price = menuItemDTO.Price,
                RestaurantId = IdRestauracji
            });
            context.SaveChangesAsync();

            return new MenuItemDTO
            {
                Name = menuItemDTO.Name,
                Price = menuItemDTO.Price,
                IdRes = IdRestauracji
            };
        }

        public int DeleteMenuItem(int itemId, PowtorkaDbContext context)
        {
            var item = context.MenuItems
                              .FirstOrDefaultAsync(m => m.Id == itemId);
            if (item is null)
                return 404;

            context.Remove(item);
            context.SaveChangesAsync();

            return 200;
        }
    }
}
