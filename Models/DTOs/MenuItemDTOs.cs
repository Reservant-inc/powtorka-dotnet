using System.ComponentModel.DataAnnotations;

namespace Powtorka.Models.DTOs
{
    public class NewMenuItemDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class GetMenuItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
    }
}
