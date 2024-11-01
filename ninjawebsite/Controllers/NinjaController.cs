using Microsoft.AspNetCore.Mvc;

namespace ninjawebsite.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ninjawebsite.Interfaces;
    using ninjawebsite.Models;
    using ninjawebsite.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;
    using System.Threading.Tasks;

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
                Gold = n.Gold
            }).ToList();

            return View(ninjaViewModels);
        }
        public async Task<IActionResult> CreateNinja(string name, int gold)
        {
            await _ninjaRepository.CreateNinja(name, gold);

            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditNinja(NinjaViewModel model)
        {
            var ninja = await _ninjaRepository.GetNinjaByIdAsync(model.Id);
            if (ninja == null)
            {
                return NotFound();
            }

            ninja.Name = model.Name;
            ninja.Gold = model.Gold;

            await _ninjaRepository.UpdateNinja(ninja);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ninja = await _ninjaRepository.GetNinjaByIdAsync(id);
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
            var ninja = await _ninjaRepository.GetNinjaByIdAsync(id);
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ninja = await _ninjaRepository.GetNinjaByIdAsync(id);
            if (ninja == null)
            {
                return NotFound();
            }

            await _ninjaRepository.DeleteNinjaAsync(id);
            return RedirectToAction("Index");
        }

    }

}
