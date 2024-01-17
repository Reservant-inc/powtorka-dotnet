namespace Powtorka.DTOs
{
    public class RestaurantDTO
    {
        public required string Name { get; set; }
        public required IEnumerable<MenuItemDTO> MenuItems { get; set; }
    }
}
