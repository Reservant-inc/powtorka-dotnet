using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Powtorka.Data;
using Powtorka.Models;
using Powtorka.Models.DTOs;
using System.Diagnostics;

namespace Powtorka.Controllers
{
    [ApiController, Route("/api/restaurants/{restaurantId: int}")]
    public class MenuController(PowtorkaDbContext _context): Controller
    {
        [HttpGet]
        [Route("/menu")]
        public async Task<IActionResult> Get(int restaurantId)
        {
            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant == null)
            {
                return NotFound();
            }

            var menu = await _context.MenuItems
                .Where(m => m.RestaurantId == restaurantId)
                .ToListAsync();

            return Ok(menu?.Select(m => new GetMenuItemDTO
            {
                Id = m.Id,
                Name = m.Name,
                Price = m.Price,
                RestaurantId = restaurantId
            }));
        }

        [HttpPost]
        [Route("/menu")]
        public async Task<IActionResult> Add(NewMenuItemDTO newMenuItem, int restaurantId)
        {

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant == null)
            {
                return NotFound();
            }

            var menuItem = new MenuItem
            {
                Name = newMenuItem.Name,
                Price = newMenuItem.Price,
                RestaurantId = restaurantId,
            };

            if (!TryValidateModel(menuItem))
            {
                return BadRequest();
            }

            if (restaurant.Menu is null)
            {
                restaurant.Menu = new List<MenuItem>() { menuItem };
            } else
            {
                restaurant.Menu.Add(menuItem);
            }

            _context.MenuItems.Add(menuItem);
            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();

            return Created("", new GetMenuItemDTO
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Price = menuItem.Price,
                RestaurantId = restaurantId
            });
        }

        [HttpDelete]
        [Route("/menu/{itemId:int}")]
        public async Task<IActionResult> Delete(int restaurantId, int itemId)
        {
            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .FirstOrDefaultAsync(m => m.Id == itemId);

            if (menuItem == null)
            {
                return NotFound();
            }

            _context.Remove(menuItem);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
