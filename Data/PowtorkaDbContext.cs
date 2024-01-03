using Microsoft.EntityFrameworkCore;

namespace Powtorka.Data;

public class PowtorkaDbContext(DbContextOptions<PowtorkaDbContext> options)
    : DbContext(options)
{
}
