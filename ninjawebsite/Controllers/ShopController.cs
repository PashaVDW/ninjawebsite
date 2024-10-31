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
        public ShopController(IShopRepository shopRepository, IEquipmentRepository equipmentRepository, INinjaRepository ninjaRepository, IInventoryRepository inventoryRepository)
        {
            _shopRepository = shopRepository;
            _equipmentRepository = equipmentRepository;
            _ninjaRepository = ninjaRepository;
            _inventoryRepository = inventoryRepository;
        }
        public async Task<IActionResult> Index(int ninjaId = 1)
        {
            var shops = await _shopRepository.GetAllShopsAsync();
            var equipment = await _equipmentRepository.GetAllEquipmentAsync();

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
                      })

                .ToList();

            return View(shopViewModels);
        }
        [HttpPost]
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
                    Category = equipment.Category
                });

                shop.IsAvailable = false;
                await _shopRepository.UpdateShopAsync(shop);
            }
            else
            {
                TempData["ToastMessage"] = "You don't have enough gold!";
                TempData["ToastType"] = "error";

                TempData["ToastId"] = "BuyMessage";
                TempData["AutoHide"] = "yes";
                TempData["MilSecHide"] = 3000;
            }
            return RedirectToAction("Index");
        }

    }
}
