using Microsoft.AspNetCore.Mvc;

namespace ninjawebsite.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
