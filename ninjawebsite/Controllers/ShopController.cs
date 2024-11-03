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
        public async Task<IActionResult> Index(int ninjaId, int categoryId = 0)
        {
            var ninja = await _ninjaRepository.GetNinjaById(ninjaId);
            if (ninja == null)
            {
                TempData["ToastMessage"] = "Ninja not found!";
                TempData["ToastType"] = "error";
                return RedirectToAction("Index", "Ninja");
            }

            var shops = await _shopRepository.GetAllShops();
            var equipment = await _equipmentRepository.GetAllEquipment();
            var categories = await _categoriesRepository.GetAllCategories();

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
                          CategoryId = e.CategoryId,
                          Strength = e.Strength,
                          Intelligence = e.Intelligence,
                          Agility = e.Agility
                      })
                .ToList();
            if (categoryId != 0)
            {
                shopViewModels = shopViewModels.Where(vm => vm.CategoryId == categoryId).ToList();
            }
            if (!shopViewModels.Any())
            {
                TempData["ToastMessage"] = "No items available for this ninja.";
                TempData["ToastType"] = "info";
            }
            var shopdata = new GroupViewModel<ShopsViewModel>
            {
                List = shopViewModels,
                Ninja = ninja,
                Categories = categories.ToList(),
                SelectedCategoryId = categoryId
            };

            return View(shopdata);
        }

        public async Task<IActionResult> Buy(int id, int ninjaId)
        {
            TempData["ToastId"] = "BuyMessage";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            var shop = await _shopRepository.GetShopById(id);
            var equipment = await _equipmentRepository.GetEquipmentById(shop.EquipmentId);
            var ninja = await _ninjaRepository.GetNinjaById(ninjaId);
            var inventories = await _inventoryRepository.GetAllInventories();

            if (inventories.Any(i => i.CategoryId == equipment.CategoryId && i.NinjaId == ninjaId))
            {
                TempData["ToastMessage"] = "You already have one width this category. Sell that one to buy this!";
                TempData["ToastType"] = "error";
                return RedirectToAction("Index", new { ninjaId });
            }
            if (equipment.GoldValue <= ninja.Gold)
            {
                ninja.Gold -= equipment.GoldValue;
                await _ninjaRepository.UpdateNinja(ninja);

                await _inventoryRepository.AddInventory(new Inventory
                {
                    EquipmentId = equipment.Id,
                    NinjaId = ninja.Id,
                    CategoryId = equipment.CategoryId
                });

                shop.IsAvailable = false;
                await _shopRepository.UpdateShop(shop);

                TempData["ToastMessage"] = $"You bought {equipment.Name}";
                TempData["ToastType"] = "success";
            }
            else
            {
                TempData["ToastMessage"] = "You don't have enough gold!";
                TempData["ToastType"] = "error";
            }

            return RedirectToAction("Index", new { ninjaId });
        }

        public async Task<IActionResult> Sell(int id, int ninjaId)
        {
            var shop = await _shopRepository.GetShopById(id);
            var ninja = await _ninjaRepository.GetNinjaById(ninjaId);
            var equipment = await _equipmentRepository.GetEquipmentById(shop.EquipmentId);
            var inventory = await _inventoryRepository.GetInventoryByEquipmentAndNinjaId(shop.EquipmentId, ninjaId);

            ninja.Gold += equipment.GoldValue;
            await _ninjaRepository.UpdateNinja(ninja);

            await _inventoryRepository.DeleteInventory(inventory);

            shop.IsAvailable = true;
            await _shopRepository.UpdateShop(shop);

            TempData["ToastMessage"] = $"You sold {equipment.Name}";
            TempData["ToastType"] = "success";
            TempData["ToastId"] = "SellMessage";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;
            return RedirectToAction("Index", new { ninjaId });
        }
    }
}
