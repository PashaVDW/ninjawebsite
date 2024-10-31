using Microsoft.AspNetCore.Mvc;

namespace ninjawebsite.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ninjawebsite.Interfaces;
    using ninjawebsite.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
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
                Naam = n.Naam,
                Goud = n.Goud
            }).ToList();

            return View(ninjaViewModels);
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
                Naam = ninja.Naam,
                Goud = ninja.Goud
            };

            return View(ninjaViewModel);
        }
    }

}
