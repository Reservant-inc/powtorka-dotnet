using Microsoft.AspNetCore.Mvc;
using Powtorka.Data;
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


        [HttpGet]
        public IActionResult getMenu(int restaurantId) {
            var MenuItems = menuService.GetMenuItems(restaurantId, context);
            return Ok(MenuItems.Result);
        }

    }
}
