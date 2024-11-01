using Microsoft.AspNetCore.Mvc;
using ninjawebsite.Interfaces;
using ninjawebsite.Models;
using ninjawebsite.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ninjawebsite.Controllers
{
    public class NinjaController : Controller
    {
        private readonly INinjaRepository _ninjaRepository;

        public NinjaController(INinjaRepository ninjaRepository)
        {
            _ninjaRepository = ninjaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ninjas = await _ninjaRepository.GetAllNinjasAsync();

            var ninjaViewModels = ninjas.Select(n => new NinjaViewModel
            {
                Id = n.Id,
                Name = n.Name,
                Gold = n.Gold,
            }).ToList();

            return View(ninjaViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNinja(string name, int gold)
        {
            var createdNinja = await _ninjaRepository.CreateNinja(name, gold);
            if (createdNinja == null)
            {
                TempData["ToastMessage"] = "Failed to create ninja. Check the inputs.";
                TempData["ToastType"] = "error";
                TempData["ToastId"] = "CreateNinjaError";
            }
            else
            {
                TempData["ToastMessage"] = "Ninja created successfully!";
                TempData["ToastType"] = "success";
                TempData["ToastId"] = "CreateNinjaSuccess";
            }
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<NinjaViewModel> GetNinjaWithEquipment(int id)
        {
            var ninja = await _ninjaRepository.GetNinjaById(id);

            if (ninja == null)
            {
                return null;
            }

            return new NinjaViewModel
            {
                Id = ninja.Id,
                Name = ninja.Name,
                Gold = ninja.Gold,
                HeadEquipment = _ninjaRepository.GetHeadEquipmentForNinja(id),
                ChestEquipment = _ninjaRepository.GetChestEquipmentForNinja(id),
                HandEquipment = _ninjaRepository.GetHandEquipmentForNinja(id),
                FeetEquipment = _ninjaRepository.GetFeetEquipmentForNinja(id),
                RingEquipment = _ninjaRepository.GetRingEquipmentForNinja(id),
                NecklaceEquipment = _ninjaRepository.GetNecklaceEquipmentForNinja(id)
            };
        }

        [HttpPost]
        public async Task<IActionResult> EditNinja(NinjaViewModel model)
        {
            var ninja = await _ninjaRepository.GetNinjaById(model.Id);
            if (ninja == null)
            {
                TempData["ToastMessage"] = "Ninja not found for editing.";
                TempData["ToastType"] = "error";
                TempData["ToastId"] = "EditNinjaError";
            }
            else
            {
                ninja.Name = model.Name;
                ninja.Gold = model.Gold;

                await _ninjaRepository.UpdateNinja(ninja);

                TempData["ToastMessage"] = "Ninja updated successfully!";
                TempData["ToastType"] = "success";
                TempData["ToastId"] = "EditNinjaSuccess";
            }
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ninja = await _ninjaRepository.GetNinjaById(id);
            if (ninja == null)
            {
                return NotFound();
            }

            var ninjaViewModel = new NinjaViewModel
            {
                Id = ninja.Id,
                Name = ninja.Name,
                Gold = ninja.Gold
            };

            return View(ninjaViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ninjaViewModel = await GetNinjaWithEquipment(id);

            if (ninjaViewModel == null)
            {
                return NotFound();
            }

            return View(ninjaViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ninja = await _ninjaRepository.GetNinjaById(id);
            if (ninja == null)
            {
                TempData["ToastMessage"] = "Ninja not found for deletion.";
                TempData["ToastType"] = "error";
                TempData["ToastId"] = "DeleteNinjaError";
            }
            else
            {
                await _ninjaRepository.DeleteNinjaAsync(id);
                TempData["ToastMessage"] = "Ninja deleted successfully.";
                TempData["ToastType"] = "success";
                TempData["ToastId"] = "DeleteNinjaSuccess";
            }
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAllEquipment(int id)
        {
            var ninjaEquipment = await _ninjaRepository.GetAllEquipmentForNinja(id);
            if (ninjaEquipment == null || !ninjaEquipment.Any())
            {
                TempData["ToastMessage"] = "No equipment to sell!";
                TempData["ToastType"] = "error";
                TempData["ToastId"] = "NoEquipment";
            }
            else
            {
                await _ninjaRepository.DeleteAllEquipmentForNinja(id);
                TempData["ToastMessage"] = "You sold all your equipment";
                TempData["ToastType"] = "success";
                TempData["ToastId"] = "SellEquipment";
            }
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 3000;

            return RedirectToAction("Details", new { id = id });
        }
    }
}
