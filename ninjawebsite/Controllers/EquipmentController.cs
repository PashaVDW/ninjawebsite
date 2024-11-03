using Microsoft.AspNetCore.Mvc;
using ninjawebsite.Interfaces;
using ninjawebsite.Models;
using ninjawebsite.Repositories;
using ninjawebsite.ViewModels;

namespace ninjawebsite.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly INinjaRepository _ninjaRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IInventoryRepository _inventoryRepository;
        public EquipmentController(IEquipmentRepository equipmentRepository, INinjaRepository ninjaRepository, ICategoriesRepository categoriesRepository, IShopRepository shopRepository, IInventoryRepository inventoryRepository)
        {
            _equipmentRepository = equipmentRepository;
            _ninjaRepository = ninjaRepository;
            _categoriesRepository = categoriesRepository;
            _shopRepository = shopRepository;
            _inventoryRepository = inventoryRepository;
        }
        public async Task<IActionResult> Index(int categoryId = 0)
        {
            var createEquipment = await _equipmentRepository.GetAllEquipment();
            var categories = await _categoriesRepository.GetAllCategories();


            var EquipmentViewModels = createEquipment.Select(e => new EquipmentViewModel
            {
                Id = e.Id,
                Name = e.Name,
                GoldValue = e.GoldValue,
                CategoryId = e.CategoryId,
                CategoryName = categories.Where(c => c.Id == e.CategoryId).FirstOrDefault().Name,
                Strength = e.Strength,
                Intelligence = e.Intelligence,
                Agility = e.Agility
            }).ToList();

            if (categoryId != 0)
            {
                EquipmentViewModels = EquipmentViewModels.Where(vm => vm.CategoryId == categoryId).ToList();
            }

            var equipmentData = new GroupViewModel<EquipmentViewModel>
            {
                List = EquipmentViewModels,
                Categories = categories.ToList(),
                SelectedCategoryId = categoryId
            };
            return View(equipmentData);
        }
        public async Task<IActionResult> Create()
        {
            var ninjas = await _ninjaRepository.GetAllNinjas();
            var categories = await _categoriesRepository.GetAllCategories();

            var EquipmentCategories = new EquipmentViewModel
            {
                Categories = categories.ToList(),
                Ninjas = ninjas.ToList()
            };

            return View(EquipmentCategories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipment(string name, int goldValue, int categoryId, int strength, int intelligence, int agility, List<int>? selectedNinjas)
        {
            TempData["ToastId"] = "CreateEquipmentMessage";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            var createEquipment = await _equipmentRepository.CreateEquipment(name, goldValue, categoryId, strength, intelligence, agility);

            if (createEquipment == null)
            {
                TempData["ToastMessage"] = "Oops, something went wrong";
                TempData["ToastType"] = "error";
                return RedirectToAction("Create");
            }

            if (selectedNinjas != null)
            {
                foreach (var ninjaId in selectedNinjas)
                {
                    await _shopRepository.CreateShopById(ninjaId, createEquipment.Id);
                }
                TempData["ToastMessage"] = "Equipment created and added to shops";
            }
            else
            {
                TempData["ToastMessage"] = "Equipment successfully created";
            }

            TempData["ToastType"] = "success";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var equipById = await _equipmentRepository.GetEquipmentById(id);
            var shops = await _shopRepository.GetAllShops();
            var categories = await _categoriesRepository.GetAllCategories();
            var ninjas = await _ninjaRepository.GetAllNinjas();
            var shopsWhereEquipmentIsIn = shops.Where(s => s.EquipmentId == id);

            EquipmentViewModel eq = new EquipmentViewModel()
            {
                Id = equipById.Id,
                Name = equipById.Name,
                CategoryId = equipById.CategoryId,
                GoldValue = equipById.GoldValue,
                Strength = equipById.Strength,
                Intelligence = equipById.Intelligence,
                Agility = equipById.Agility,
                Categories = categories.ToList(),
                Ninjas = ninjas.ToList(),
                Shops = shopsWhereEquipmentIsIn.ToList()
            };
            return View("Edit", eq);
        }
        [HttpPost]
        public async Task<IActionResult> EditEquipment(int id, string name, decimal goldValue, int categoryId, int strength, int intelligence, int agility, List<int>? selectedNinjas)
        {
            TempData["ToastId"] = "CreateEquipmentMessage";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;
            var eq = await _equipmentRepository.GetEquipmentById(id);

            if (selectedNinjas != null)
            {
                var shops = await _shopRepository.GetAllShops();
                foreach (var ninjaId in selectedNinjas)
                {
                    if (!shops.Any(s => s.EquipmentId == id && s.NinjaId == ninjaId))
                    {
                        await _shopRepository.CreateShopById(ninjaId, id);
                    }
                }
                var eqShops = shops.Where(s => s.EquipmentId == id);
                if (eqShops != null)
                {
                    foreach (var shop in eqShops)
                    {
                        if (!selectedNinjas.Any(n => n == shop.NinjaId))
                        {
                            var ninja = await _ninjaRepository.GetNinjaById(shop.NinjaId);
                            var inventory = await _inventoryRepository.GetInventoryByEquipmentAndNinjaId(shop.EquipmentId, shop.NinjaId);
                            if (inventory != null)
                            {
                                await _inventoryRepository.DeleteInventory(inventory);
                                ninja.Gold += eq.GoldValue;
                                await _ninjaRepository.UpdateNinja(ninja);
                            }
                            await _shopRepository.DeleteShop(shop);
                        }
                    }
                }
                TempData["ToastMessage"] = "Equipment created and added to shop";
            }
            else
            {
                TempData["ToastMessage"] = "Equipment successfully updated";
            }
            eq.Name = name;
            eq.GoldValue = goldValue;
            eq.CategoryId = categoryId;
            eq.Strength = strength;
            eq.Intelligence = intelligence;
            eq.Agility = agility;
            var createEquipment = await _equipmentRepository.UpdateEquipment(eq);

            if (createEquipment == null)
            {
                TempData["ToastMessage"] = "Oops, something went wrong";
                TempData["ToastType"] = "error";
                return RedirectToAction("Create");
            }

            TempData["ToastType"] = "success";
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            TempData["ToastId"] = "DeleteEquipmentMessage";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;
            TempData["ToastMessage"] = "Equipment deleted!";

            var equipment = await _equipmentRepository.GetEquipmentById(id);

            if (equipment == null)
            {
                TempData["ToastMessage"] = "Equipment not found!";
                TempData["ToastType"] = "error";
                return RedirectToAction("Index");
            }

            var shops = await _shopRepository.GetAllShops();
            var eqInShops = shops.Where(s => s.EquipmentId == id);
            if (eqInShops.Any())
            {
                foreach (var shop in eqInShops)
                {
                    var ninja = await _ninjaRepository.GetNinjaById(shop.NinjaId);
                    var inventory = await _inventoryRepository.GetInventoryByEquipmentAndNinjaId(shop.EquipmentId, shop.NinjaId);
                    if (inventory != null)
                    {
                        await _inventoryRepository.DeleteInventory(inventory);
                        ninja.Gold += equipment.GoldValue;
                        await _ninjaRepository.UpdateNinja(ninja);
                    }
                    await _shopRepository.DeleteShop(shop);
                }
                TempData["ToastMessage"] = "Equipment deleted and gold added to the ninja's!";
                TempData["ToastType"] = "success";
            }
            TempData["ToastType"] = "success";
            await _equipmentRepository.DeleteEquipment(equipment);
            return RedirectToAction("Index");
        }

    }
}
