using System.ComponentModel.DataAnnotations;

namespace Powtorka.Models;

public class Restaurant
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(70)]
    public string Name { get; set; } = null!;

    [Required]
    public DateTime AddedAt { get; set; }

    public ICollection<MenuItem>? Menu { get; set; }
}
