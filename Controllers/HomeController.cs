using DormitoryManager.Models;
using DormitoryManager.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DormitoryManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DormitoryManagerContext _context;

        public HomeController(ILogger<HomeController> logger, DormitoryManagerContext context)
        {

            _context = context; 
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
        public IActionResult Creators()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }
        public IActionResult Documents()
        {
            var dormitories = _context.Dormitories.ToList();
            return View(dormitories);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
