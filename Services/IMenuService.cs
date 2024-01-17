using Powtorka.Data;
using Powtorka.DTOs;

namespace Powtorka.Services
{
    public interface IMenuService
    {
        public Task<List<MenuItemDTO>> GetMenuItems(int IdRestauracji, PowtorkaDbContext context);
    }
}
