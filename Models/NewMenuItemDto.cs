using System.ComponentModel.DataAnnotations;

namespace Powtorka.Models
{
    public class NewMenuItemDto
    {
        [Required, MaxLength(70)]
        public string Name { get; set; }

        [Required, Range(0, 200)]
        public decimal Price { get; set; }
    }
}
