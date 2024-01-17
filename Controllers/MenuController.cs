using Microsoft.AspNetCore.Mvc;

namespace Powtorka.Controllers
{
    /// <summary>
    /// Kontroler obługujący menu w restauracjach.
    /// </summary>
    [Route("/api/restaurants/{restaurantId:int}/menu")]
    [ApiController]
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
