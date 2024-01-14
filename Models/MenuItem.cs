using System.ComponentModel.DataAnnotations;

namespace Powtorka.Models;

public class MenuItem
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(70)]
    public string Name { get; set; } = null!;

    [Required, Range(0, 200)]
    public decimal Price { get; set; }

    public int RestaurantId { get; set; }

    public Restaurant? Restaurant { get; set; }
}
