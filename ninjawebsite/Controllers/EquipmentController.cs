using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ninjawebsite.Interfaces;
using ninjawebsite.Models;
using ninjawebsite.Repositories;
using ninjawebsite.ViewModels;
using static System.Formats.Asn1.AsnWriter;

namespace ninjawebsite.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly INinjaRepository _ninjaRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        public EquipmentController(IEquipmentRepository equipmentRepository, INinjaRepository ninjaRepository, ICategoriesRepository categoriesRepository)
        {
            _equipmentRepository = equipmentRepository;
            _ninjaRepository = ninjaRepository;
            _categoriesRepository = categoriesRepository;
        }
        public async Task<IActionResult> Index(int ninjaId = 1, int categoryId = 0)
        {
            var createEquipment = await _equipmentRepository.GetAllEquipmentAsync();
            var ninja = await _ninjaRepository.GetNinjaByIdAsync(ninjaId);
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
        public async Task<IActionResult> CreateEquipment(string name, int goldValue, int categoryId, int strength, int intelligence, int agility)
        {
            var createEquipment = await _equipmentRepository.CreateEquipment(name, goldValue, categoryId, strength, intelligence, agility);
            if (createEquipment == null)
            {
                TempData["ToastMessage"] = "Oops something went wrong";
                TempData["ToastType"] = "success";

                return RedirectToAction("Create");
            }
            TempData["ToastMessage"] = "Equipment succesfully made";
            TempData["ToastType"] = "success";
            TempData["ToastId"] = "CreateEquipmentMessage";
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;
            return RedirectToAction("Index");
        }

    }
}
