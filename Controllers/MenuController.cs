using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Powtorka.Data;
using Powtorka.Models;
using Powtorka.Models.DTOs;

namespace Powtorka.Controllers
{
    [ApiController, Route("/api/restaurants/{restaurantId: int}")]
    public class MenuController(PowtorkaDbContext _context): Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get(int restaurantId)
        {
            var menuItems = await _context.MenuItems
                .Where(m => m.RestaurantId == restaurantId)
                .ToListAsync();

            return Ok(menuItems.Select(m => new GetMenuItemDTO
            {
                Id = m.Id,
                Name = m.Name,
                Price = m.Price,
                RestaurantId = restaurantId
            }));
        }
    }
}
