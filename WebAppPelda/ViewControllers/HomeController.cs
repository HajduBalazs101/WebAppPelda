using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebAppPelda.Models;
using WebAppPelda.Services;

namespace WebAppPelda.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Sajat()
        {
            List<Vasarlo> vasarlok = new VasarloService().GetAllVasarlo();
            return View(vasarlok);
        }

        public IActionResult Vasarlo(int id)
        {
            Vasarlo vasarlo = new VasarloService().GetById(id);
            return View(vasarlo);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult CreateVasarlo()
        {
            Vasarlo uresVasarlo = new Vasarlo();
            return View(uresVasarlo);
        }
        [HttpPost]
        public IActionResult CreateVasarlo(Vasarlo vasarlo)
        {
            string result = new VasarloService().PostVasarlo(vasarlo);
            TempData["SuccessMessage"] = result;
            return RedirectToAction(nameof(CreateVasarlo));
        }

        public IActionResult EditVasarlo(int id)
        {
            Vasarlo vasarlo = new VasarloService().GetById(id);
            return View(vasarlo);
        }

        [HttpPost]
        public IActionResult EditVasarlo(Vasarlo vasarlo)
        {
            string result = new VasarloService().PutVasarlo(vasarlo);
            TempData["SuccessMessage"] = result;
            return RedirectToAction(nameof(EditVasarlo));
        }

        [HttpPost]
        public IActionResult DeleteVasarlo(int id)
        {
            string result = new VasarloService().DeleteVasarlo(id);
            TempData["SuccessMessage"] = result;
            return RedirectToAction(nameof(Sajat));
        }
    }
}
