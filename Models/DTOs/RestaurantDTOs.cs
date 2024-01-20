namespace Powtorka.Models.DTOs
{
    public class RestaurantGetAllDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AddedAt { get; set; }
    }

    public class NewRestaurantDto
    {
        public string Name { get; set; }
        public ICollection<NewMenuItemDTO>? Menu { get; set; }
    }
}
