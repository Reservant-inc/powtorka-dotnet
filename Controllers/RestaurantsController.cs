using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Powtorka.Data;
using Powtorka.Models;
using Powtorka.Models.DTOs;

namespace Powtorka.Controllers;

[ApiController, Route("/api/restaurants")]
public class RestaurantsController(PowtorkaDbContext context) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await context.Restaurants
            .Select(r => new RestaurantGetAllDTO()
            {
                Id = r.Id,
                Name = r.Name,
                AddedAt = r.AddedAt
            }).ToListAsync() );
    }

    [HttpPost]
    public async Task<IActionResult> AddNew(NewRestaurantDto data)
    {

        var restaurant = new Restaurant
        {
            Name = data.Name,
            AddedAt = DateTime.Now,
            Menu = data.Menu?.Select(r => new MenuItem
            {
                Name = r.Name,
                Price = r.Price,
            }).ToList()
        };


        if (!TryValidateModel(restaurant))
        {
            return BadRequest(ModelState);
        }

        context.Add(restaurant);
        await context.SaveChangesAsync();


        //czy zwracajac obiekt przy created tez musi byc DTO?
        return Created(
            $"/api/restaurants/{restaurant.Id}",
            new
            {
                restaurant.Id,
                restaurant.Name,
                restaurant.AddedAt,
                Menu = restaurant.Menu?.Select(m => new
                {
                    m.Id,
                    m.Name,
                    m.Price,
                    m.RestaurantId
                }).ToList()
            });

    }

    /*[HttpGet, Route("{id:int}")]
    public async Task<IActionResult> GetDetails(int id)
    {
        
    }*/

    /*[HttpDelete, Route("{id:int}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        
    }*/
}
