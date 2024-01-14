using Microsoft.EntityFrameworkCore;
using Powtorka.Models;

namespace Powtorka.Data;

public class PowtorkaDbContext(DbContextOptions<PowtorkaDbContext> options)
    : DbContext(options)
{
    public DbSet<Restaurant> Restaurants { get; set; } = null!;
    public DbSet<MenuItem> MenuItems { get; set; } = null!;
}
