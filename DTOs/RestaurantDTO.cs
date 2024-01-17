namespace Powtorka.DTOs
{
    public class RestaurantDTO
    {
        public required string Name { get; set; }
        public required List<MenuItemDTO> MenuItems { get; set; }
    }
}
