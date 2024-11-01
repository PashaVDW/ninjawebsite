using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public EquipmentController(IEquipmentRepository equipmentRepository, INinjaRepository ninjaRepository, ICategoriesRepository categoriesRepository, IShopRepository shopRepository)
        {
            _equipmentRepository = equipmentRepository;
            _ninjaRepository = ninjaRepository;
            _categoriesRepository = categoriesRepository;
            _shopRepository = shopRepository;
        }
        public async Task<IActionResult> Index(int ninjaId = 6, int categoryId = 0)
        {
            var createEquipment = await _equipmentRepository.GetAllEquipmentAsync();
            var ninja = await _ninjaRepository.GetNinjaById(ninjaId);
            var categories = await _categoriesRepository.GetAllCategoriesAsync();

            var EquipmentViewModels = createEquipment.Select(e => new EquipmentViewModel
            {
                Id = e.Id,
                Name = e.Name,
                GoldValue = e.GoldValue,
                CategoryId = e.CategoryId,
                Strength = e.Strength,
                Intelligence = e.Intelligence,
                Agility = e.Agility
            }).ToList();

            ViewBag.selectedCategoryId = categoryId;
            ViewBag.ninja = ninja;
            ViewBag.categories = categories;
            if (categoryId != 0)
            {
                EquipmentViewModels = EquipmentViewModels.Where(vm => vm.CategoryId == categoryId).ToList();
            }
            return View(EquipmentViewModels);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _categoriesRepository.GetAllCategoriesAsync();
            ViewBag.categories = categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEquipment(string name, int goldValue, int categoryId, int strength, int intelligence, int agility, bool addToShop)
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

            if (addToShop)
            {
                var ninja = await _ninjaRepository.GetNinjaById(1);
                await _shopRepository.CreateShopById(ninja.Id, createEquipment.Id);
                TempData["ToastMessage"] = "Equipment created and added to shop";
            }
            else
            {
                TempData["ToastMessage"] = "Equipment successfully created";
            }

            TempData["ToastType"] = "success";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id, int ninjaId = 1)
        {
            bool addToShop = false;
            var equipById = await _equipmentRepository.GetEquipmentByIdAsync(id);
            var shops = await _shopRepository.GetAllShopsAsync();

            if (shops.Where(s => s.EquipmentId == id && s.NinjaId == ninjaId).FirstOrDefault() != null)
            {
                addToShop = true;
            }
            EquipmentViewModel eq = new EquipmentViewModel()
            {
                Id = equipById.Id,
                Name = equipById.Name,
                CategoryId = equipById.CategoryId,
                GoldValue = equipById.GoldValue,
                Strength = equipById.Strength,
                Intelligence = equipById.Intelligence,
                Agility = equipById.Agility,
                AddToShop = addToShop
            };
            var categories = await _categoriesRepository.GetAllCategoriesAsync();
            ViewBag.categories = categories;
            return View("Edit", eq);
        }
        [HttpPost]
        public async Task<IActionResult> EditEquipment(int id, string name, int goldValue, int categoryId, int strength, int intelligence, int agility, bool addToShop)
        {
            TempData["ToastId"] = "CreateEquipmentMessage";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;
            var eq = await _equipmentRepository.GetEquipmentByIdAsync(id);

            if (addToShop)
            {

                //var ninja = await _ninjaRepository.GetNinjaById(1);
                //var existingShop = await _shopRepository.ge
                //await _shopRepository.CreateShopById(ninja.Id, createEquipment.Id);
                TempData["ToastMessage"] = "Equipment created and added to shop";
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

            else
            {
                TempData["ToastMessage"] = "Equipment successfully created";
            }

            TempData["ToastType"] = "success";
            return RedirectToAction("Index");

        }
    }
}
