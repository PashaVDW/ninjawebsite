using Microsoft.AspNetCore.Mvc;
using ninjawebsite.Interfaces;
using ninjawebsite.Repositories;
using ninjawebsite.ViewModels;

namespace ninjawebsite.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopRepository _shopRepository;

        public ShopController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<IActionResult> Index()
        {
            var shops = await _shopRepository.GetAllShopsAsync();

            //var shopViewModels = shops.Select(s => new ShopsViewModel
            //{

            //});

            return View(shops);
        }

    }
}
