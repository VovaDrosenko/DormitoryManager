using DormitoryManager.Interfaces;
using DormitoryManager.Models;
using DormitoryManager.Models.DTO_s.Student;
using DormitoryManager.Validation.Student;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DormitoryManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IFacultyService _facultyService;

        public HomeController(IStudentService studentService, IFacultyService facultyService)
        {
            _studentService = studentService;
            _facultyService = facultyService;
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

        public async Task <IActionResult> Form()
        {
            var faculties = await _facultyService.GettAll();
            ViewBag.Faculties = faculties;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Form(StudentsDto student)
        {
            var validator = new CreateStudentValidation();
            var validationResult = await validator.ValidateAsync(student);
            if (validationResult.IsValid)
            {
                await _studentService.Create(student);
                
                return RedirectToAction(nameof(Index));
                
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View(student);
        }

        public IActionResult Documents()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
