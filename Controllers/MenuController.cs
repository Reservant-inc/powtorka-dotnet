using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Powtorka.Data;
using Powtorka.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Powtorka.Controllers
{
    [ApiController, Route("/api/menuitems")]
    public class MenuItemsController : Controller
    {
        private readonly PowtorkaDbContext context;

        public MenuItemsController(PowtorkaDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await context.MenuItems
                .Select(item => new
                {
                    item.Id,
                    item.Name,
                    item.Price,
                    item.RestaurantId
                })
                .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMenuItem(int restaurantId, NewMenuItemDto data)
        {
            var restaurant = await context.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
            {
                return NotFound($"Restaurant with ID {restaurantId} not found.");
            }

            var newMenuItem = new MenuItem
            {
                Name = data.Name,
                Price = data.Price,
                RestaurantId = restaurantId
            };

            if (!TryValidateModel(newMenuItem))
            {
                return BadRequest(ModelState);
            }

            context.MenuItems.Add(newMenuItem);
            await context.SaveChangesAsync();

            return Created(
                $"/api/restaurants/{restaurantId}/menu/{newMenuItem.Id}",
                new
                {
                    newMenuItem.Id,
                    newMenuItem.Name,
                    newMenuItem.Price,
                    RestaurantId = restaurantId
                });
        }


        [HttpGet, Route("{id:int}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var menuItem = await context.MenuItems
                .FirstOrDefaultAsync(item => item.Id == id);

            if (menuItem is null)
            {
                return NotFound($"MenuItem with ID {id} not found");
            }

            return Ok(new
            {
                menuItem.Id,
                menuItem.Name,
                menuItem.Price,
                menuItem.RestaurantId
            });
        }

        [HttpDelete, Route("{id:int}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var menuItem = await context.MenuItems
                .FirstOrDefaultAsync(item => item.Id == id);

            if (menuItem is null)
            {
                return NotFound($"MenuItem with ID {id} not found");
            }

            context.MenuItems.Remove(menuItem);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
