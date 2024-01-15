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


        // POST Dodaje nowy MenuItem do wybranej restauracji.
        [HttpPost, Route("{restaurantId:int}/menu")]
        public async Task<IActionResult> AddMenuItem(NewMenuItemDto data, int restaurantId)
        {

            var restaurant = await context.Restaurants
                .Include(r => r.Menu)
                .SingleOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant == null) return NotFound($"Nie znaleziono restauracji o id: {restaurantId}.");

            var newMenu = new MenuItem
            {
                Name = data.Name,
                Price = data.Price
            };

            if (!TryValidateModel(newMenu)) return BadRequest(ModelState);

            context.MenuItems.Add(newMenu);
            restaurant.Menu.Add(newMenu);

            await context.SaveChangesAsync();

            return Created(
                $"/api/restaurants/{restaurantId}",
                restaurant
                );
        }

        // DELETE Usuwa wybrany MenuItem.
        [HttpDelete, Route("{restaurantId:int}/menu/{itemId:int}")]
        public async Task<IActionResult> DeleteMenuItem(int restaurantId, int itemId)
        {
            var menuItem = await context.MenuItems
                .FirstOrDefaultAsync(m => m.Id == itemId);

            if (menuItem is null) return NotFound($"MenuItem o ID {itemId} nie zostało znalezione.");


            var restaurant = await context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant is null) return NotFound($"Restauracja o ID {restaurantId} nie została znaleziona.");

            // Usuwanie
            context.MenuItems.Remove(menuItem);
            restaurant.Menu.Remove(menuItem);

            await context.SaveChangesAsync();
             
            return NoContent();
        }

    }
}
