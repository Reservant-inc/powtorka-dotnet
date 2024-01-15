using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Powtorka.Data;
using Powtorka.Models;

namespace Powtorka.Controllers
{


    [ApiController, Route("/api/restaurants")]
    public class MenuController(PowtorkaDbContext context) : Controller
    {

        // GET Zwraca wszystkie MenuItem przypisane do danej restauracji.
        [HttpGet, Route("{restaurantId:int}/menu")]
        public async Task<IActionResult> GetMenuItem(int restaurantId)
        {
            return Ok(await context.MenuItems
                .Where(m => m.RestaurantId == restaurantId)
                .ToListAsync()
            );
        }



    }
}
