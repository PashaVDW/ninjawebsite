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
        public async Task<IActionResult> Index(int ninjaId = 1, int categoryId = 0)
        {
            var shops = await _shopRepository.GetAllShopsAsync();
            var equipment = await _equipmentRepository.GetAllEquipmentAsync();
            var ninja = await _ninjaRepository.GetNinjaByIdAsync(ninjaId);
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
            if (categoryId != 0)
            {
                shopViewModels = shopViewModels.Where(vm => vm.CategoryId == categoryId).ToList();
            }
            ViewBag.selectedCategoryId = categoryId;
            ViewBag.ninja = ninja;
            ViewBag.categories = categories;

            return View(shopViewModels);
        }
        public async Task<IActionResult> Buy(int id, int ninjaId = 1)
        {
            var shop = await _shopRepository.GetShopByIdAsync(id);
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(shop.EquipmentId);
            var ninja = await _ninjaRepository.GetNinjaByIdAsync(ninjaId);

            if (equipment.GoldValue <= ninja.Gold)
            {
                var newGoldAmount = ninja.Gold - equipment.GoldValue;
                ninja.Gold = newGoldAmount;
                await _ninjaRepository.UpdateNinjaAsync(ninja);

                await _inventoryRepository.AddInvertoryAsync(new Inventory
                {
                    EquipmentId = equipment.Id,
                    NinjaId = ninja.Id,
                    CategoryId = equipment.CategoryId
                });

                shop.IsAvailable = false;
                await _shopRepository.UpdateShopAsync(shop);
                TempData["ToastMessage"] = "You bought " + equipment.Name;
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
            return RedirectToAction("Index");
        }
        //public async Task Task<IActionResult> Sell(int id, int ninjaId = 1)
        //{
        //    var inventory = await _inventoryRepository.GetInventoryByIdAsync(id);
        //    var ninja = await _ninjaRepository.GetNinjaByIdAsync(ninjaId);
        //    var equipment = await _equipmentRepository.GetEquipmentByIdAsync(inventory.EquipmentId);

        //    ninja.Gold += equipment.GoldValue;
        //    await _ninjaRepository.UpdateNinjaAsync(ninja);

        //    await _inventoryRepository.DeleteInventoryAsync(inventory);

        //    TempData["ToastMessage"] = "You sold " + equipment.Name;
        //    TempData["ToastType"] = "success";
        //    TempData["ToastId"] = "SellMessage";
        //    TempData["AutoHide"] = "yes";
        //    TempData["MilSecHide"] = 3000;
        //    return RedirectToAction("Index");
        //}


    }
}
