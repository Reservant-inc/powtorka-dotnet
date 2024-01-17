using Microsoft.AspNetCore.Mvc;
using Powtorka.Data;
using Powtorka.DTOs;

namespace Powtorka.Services
{
    public interface IMenuService
    {
        public Task<List<MenuItemDTO>> GetMenuItems(int IdRestauracji, PowtorkaDbContext context);

        public MenuItemDTO AddMenuItem(int IdRestauracji, PowtorkaDbContext context, MenuItemDTO menuItemDTO);

        public int DeleteMenuItem(int itemId, PowtorkaDbContext context);
    }
}
