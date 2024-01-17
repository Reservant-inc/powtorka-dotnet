using Microsoft.AspNetCore.Mvc;

namespace Powtorka.Controllers
{
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
