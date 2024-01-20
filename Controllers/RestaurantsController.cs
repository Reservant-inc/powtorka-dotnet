using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Powtorka.Data;
using Powtorka.Models;
using Powtorka.Models.DTOs;
using System.Diagnostics;

namespace Powtorka.Controllers;

[ApiController, Route("/api/restaurants")]
public class RestaurantsController(PowtorkaDbContext context) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await context.Restaurants
            .Select(r => new GetAllRestaurantDTO()
            {
                Id = r.Id,
                Name = r.Name,
                AddedAt = r.AddedAt
            }).ToListAsync() );
    }

    [HttpPost]
    public async Task<IActionResult> AddNew(NewRestaurantDto data)
    {
        var menu = data.Menu?.Select(r => new MenuItem
        {
            Name = r.Name,
            Price = r.Price,
        }).ToList();

        var restaurant = new Restaurant
        {
            Name = data.Name,
            AddedAt = DateTime.Now,
            Menu = menu
        };


        if (!TryValidateModel(restaurant) || (menu is not null && !TryValidateModel(menu)))
        {
            return BadRequest(ModelState);
        }


        context.Add(restaurant);

        if (menu is not null) { context.AddRange(menu); }

        await context.SaveChangesAsync();

        return Created(
            $"/api/restaurants/{restaurant.Id}",
            new GetRestaurantDTO
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                AddedAt = restaurant.AddedAt,
                Menu = restaurant.Menu?.Select(m => new GetMenuItemDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Price = m.Price,
                    RestaurantId = m.RestaurantId
                }).ToList()
            });

    }

    [HttpGet, Route("{id:int}")]
    public async Task<IActionResult> GetDetails(int id)
    {

        var restaurant = await context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);

        var menu = await context.MenuItems.Where(m => m.RestaurantId == id).ToListAsync();

        if (restaurant == null)
        {
            return NotFound();
        }

        return Ok(new GetRestaurantDTO
        {
            Id = id,
            Name = restaurant.Name,
            AddedAt = restaurant.AddedAt,
            Menu = menu?.Select(m => new GetMenuItemDTO
            {
                Id = m.Id,
                Name = m.Name,
                Price = m.Price,
                RestaurantId = id
            }).ToList()
        });
    }

    [HttpDelete, Route("{id:int}")]
    public async Task<IActionResult> DeleteItem(int id)
    {

        var restaurant = context.Restaurants.FirstOrDefault(r => r.Id == id);

        if (restaurant == null)
        {
            return NotFound();
        }

        context.Remove(restaurant);

        if (restaurant.Menu is not null)
        {
            context.RemoveRange(restaurant.Menu);
        }

        await context.SaveChangesAsync();

        return Ok();

    }
}
