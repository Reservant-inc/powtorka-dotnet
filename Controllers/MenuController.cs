using Microsoft.AspNetCore.Mvc;
using Powtorka.Data;
using Powtorka.DTOs;
using Powtorka.Services;

namespace Powtorka.Controllers
{
    /// <summary>
    /// Kontroler obługujący menu w restauracjach.
    /// </summary>
    [Route("/api/restaurants/{restaurantId:int}/menu")]
    [ApiController]
    public class MenuController(PowtorkaDbContext context, IMenuService menuService) : Controller
    {
        private readonly string Address = "/api/restaurants/{restaurantId:int}/menu";

        [HttpGet]
        public IActionResult getMenu(int restaurantId) {
            var MenuItems = menuService.GetMenuItems(restaurantId, context);
            return Ok(MenuItems.Result);
        }

        [HttpPost]
        public IActionResult postMenuItem(int restaurantId, [FromBody] MenuItemDTO menuItemDTO) {
            var res = menuService.AddMenuItem(restaurantId, context, menuItemDTO);
            return Created(Address, res);
        }

    }
}
