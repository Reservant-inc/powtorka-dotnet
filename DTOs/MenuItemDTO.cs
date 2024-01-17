namespace Powtorka.DTOs
{
    public class MenuItemDTO
    {
        public required string Name {  get; set; }
        public required decimal Price { get; set; }
        public int? IdRes { get; set; } 
    }
}
