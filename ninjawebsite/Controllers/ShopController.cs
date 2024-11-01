using Microsoft.AspNetCore.Mvc;
using ninjawebsite.Interfaces;
using ninjawebsite.Models;
using ninjawebsite.Repositories;
using ninjawebsite.ViewModels;

namespace ninjawebsite.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopRepository _shopRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly INinjaRepository _ninjaRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        public ShopController(IShopRepository shopRepository, IEquipmentRepository equipmentRepository, INinjaRepository ninjaRepository, IInventoryRepository inventoryRepository, ICategoriesRepository categoriesRepository)
        {
            _shopRepository = shopRepository;
            _equipmentRepository = equipmentRepository;
            _ninjaRepository = ninjaRepository;
            _inventoryRepository = inventoryRepository;
            _categoriesRepository = categoriesRepository;
        }
        public async Task<IActionResult> Index(int ninjaId)
        {
            var ninja = await _ninjaRepository.GetNinjaById(ninjaId);
            if (ninja == null)
            {
                TempData["ToastMessage"] = "Ninja not found!";
                TempData["ToastType"] = "error";
                return RedirectToAction("Index", "Ninja");
            }

            var shops = await _shopRepository.GetAllShopsAsync();
            var equipment = await _equipmentRepository.GetAllEquipmentAsync();
            var categories = await _categoriesRepository.GetAllCategoriesAsync();

            var shopViewModels = shops
                .Where(s => s.NinjaId == ninjaId)
                .Join(equipment,
                      s => s.EquipmentId,
                      e => e.Id,
                      (s, e) => new ShopsViewModel
                      {
                          Id = s.Id,
                          EqId = e.Id,
                          Name = e.Name,
                          Gold = e.GoldValue,
                          IsAvailable = s.IsAvailable,
                          CategoryName = e.Category.Name,
                          CategoryId = e.CategoryId
                      })
                .ToList();

            if (!shopViewModels.Any())
            {
                TempData["ToastMessage"] = "No items available for this ninja.";
                TempData["ToastType"] = "info";
            }

            ViewBag.ninja = ninja;
            ViewBag.categories = categories;

            return View(shopViewModels);
        }

        public async Task<IActionResult> Buy(int id, int ninjaId)
        {
            var shop = await _shopRepository.GetShopByIdAsync(id);
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(shop.EquipmentId);
            var ninja = await _ninjaRepository.GetNinjaById(ninjaId);

            if (equipment.GoldValue <= ninja.Gold)
            {
                ninja.Gold -= equipment.GoldValue;
                await _ninjaRepository.UpdateNinja(ninja);

                await _inventoryRepository.AddInventoryAsync(new Inventory
                {
                    EquipmentId = equipment.Id,
                    NinjaId = ninja.Id,
                    CategoryId = equipment.CategoryId
                });

                shop.IsAvailable = false;
                await _shopRepository.UpdateShopAsync(shop);

                TempData["ToastMessage"] = $"You bought {equipment.Name}";
                TempData["ToastType"] = "success";
            }
            else
            {
                TempData["ToastMessage"] = "You don't have enough gold!";
                TempData["ToastType"] = "error";
            }

            TempData["ToastId"] = "BuyMessage";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;
            return RedirectToAction("Index", new { ninjaId });
        }

        public async Task<IActionResult> Sell(int id, int ninjaId)
        {
            var shop = await _shopRepository.GetShopByIdAsync(id);
            var ninja = await _ninjaRepository.GetNinjaById(ninjaId);
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(shop.EquipmentId);
            var inventory = await _inventoryRepository.GetInventoryByEquipmentAndNinjaIdAsync(shop.EquipmentId, ninjaId);

            ninja.Gold += equipment.GoldValue;
            await _ninjaRepository.UpdateNinja(ninja);

            await _inventoryRepository.DeleteInventoryAsync(inventory);

            shop.IsAvailable = true;
            await _shopRepository.UpdateShopAsync(shop);

            TempData["ToastMessage"] = $"You sold {equipment.Name}";
            TempData["ToastType"] = "success";
            TempData["ToastId"] = "SellMessage";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;
            return RedirectToAction("Index", new { ninjaId });
        }
    }
}
