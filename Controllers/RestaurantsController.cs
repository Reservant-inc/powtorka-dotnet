using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Powtorka.Data;
using Powtorka.Models;

namespace Powtorka.Controllers;

public record NewMenuItemDto(string Name, decimal Price);
public record NewRestaurantDto(string Name, List<NewMenuItemDto> Menu);

[ApiController, Route("/api/restaurants")]
public class RestaurantsController(PowtorkaDbContext context) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await context.Restaurants
            .Select(r => new
            {
                r.Id,
                r.Name,
                r.AddedAt
            })
            .ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddNew(NewRestaurantDto data)
    {
        var newRestaurant = new Restaurant
        {
            Name = data.Name,
            AddedAt = DateTime.Now
        };

        newRestaurant.Menu = new List<MenuItem>();
        foreach (var item in data.Menu)
        {
            newRestaurant.Menu.Add(new MenuItem
            {
                Name = item.Name,
                Price = item.Price
            });
        }

        if (!TryValidateModel(newRestaurant))
        {
            return BadRequest(ModelState);
        }

        context.Restaurants.Add(newRestaurant);
        await context.SaveChangesAsync();

        return Created(
            $"/api/restaurants/{newRestaurant.Id}",
            new
            {
                newRestaurant.Id,
                newRestaurant.Name,
                newRestaurant.AddedAt,
                Menu = newRestaurant.Menu
                    .Select(i => new
                    {
                        i.Name,
                        i.Price
                    })
            });
    }

    [HttpGet, Route("{id:int}")]
    public async Task<IActionResult> GetDetails(int id)
    {
        var restaurant = await context.Restaurants
            .Include(r => r.Menu)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (restaurant is null)
        {
            return NotFound($"Restauracja o ID {id} nie została znaleziona");
        }

        return Ok(new
        {
            restaurant.Id,
            restaurant.Name,
            restaurant.AddedAt,
            Menu = restaurant.Menu!
                .Select(i => new
                {
                    i.Name,
                    i.Price
                })
        });
    }

    [HttpDelete, Route("{id:int}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var restaurant = await context.Restaurants
            .Include(r => r.Menu)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (restaurant is null)
        {
            return NotFound($"Restauracja o ID {id} nie została znaleziona");
        }

        context.Restaurants.Remove(restaurant);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
