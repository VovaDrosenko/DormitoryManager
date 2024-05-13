using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using DormitoryManager.Models.DTO_s.User;
using DormitoryManager.Services;
using DormitoryManager.Valudation.User;
using DormitoryManager.Validation.User;
using DormitoryManager.Services.User;
using DormitoryManager.Services.Student;
using DormitoryManager.Interfaces;
using DormitoryManager.Models.DTO_s.Student;
using DormitoryManager.Models.Entities;
using DormitoryManager.Services.Faculty;

namespace DormitoryManager.Controllers
{

    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserService _userService;
        private readonly IStudentService _studentService;
        private readonly IFacultyService _facultyService;



        public DashboardController(UserService userService, IStudentService studentService, IFacultyService facultyService)
        {
            _userService = userService;
            _studentService = studentService;
            _facultyService = facultyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous] // Method GET
        public async Task<IActionResult> SignIn()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            if (user)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        [AllowAnonymous] // Method POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginUserDto model)
        {
            var validator = new LoginUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _userService.LoginUserAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.AuthError = result.Message;
                return View(model);
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View(model);
        }

        public async Task<IActionResult> GetAll()
        {
            var result = await _studentService.GettAllSettStud();
            return View(result);
        }
        public async Task<IActionResult> Requests()
        {
            var students = await _studentService.GetAllRequest();
            // Assuming _facultyRepository is your repository for faculties
            var faculties = await _facultyService.GettAll();
            var result = from s in students join f in faculties on s.FacultyId equals f.Id
                         select new StudentsDto
                         {
                             Id = s.Id,
                             StudentLastname = s.StudentLastname,
                             StudentMiddlename = s.StudentMiddlename,
                             StudentName = s.StudentName,
                             StudentPhone = s.StudentPhone,
                             FacultyId = s.FacultyId,
                             Gender = s.Gender,
                             FacultyName = f.FacultyName
                         };

            return View(result.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutUserAsync();
            return RedirectToAction(nameof(SignIn));
        }

        

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDto model)
        {
            var validator = new CreateUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.CreateAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.AuthError = result.Payload;
                return View(model);
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View(model);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _studentService.Delete(Id);
            
                return View(nameof(Index));
            

        }


        public async Task<IActionResult> DeleteById(string id)
        {
            var result = await _userService.DeleteAsync(id);
            if (result.Success)
            {
                return View(nameof(Index));
            }
            return View(nameof(Index));
        }

        /*[HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var result = await _userService.GetByIdAsync(id);
            await GetRoles();
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View();
        }*/

        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            var result = await _studentService.Get(id);
            await GetRoles();
            if (result != null)
            {
                return View(result);
            }
            return View();
        }
        private async Task GetRoles()
        {
            var result = await _userService.LoadRoles();
            @ViewBag.RoleList = new SelectList(
          result, nameof(IdentityRole.Id),
              nameof(IdentityRole.Name)
              );
        }
        [HttpPost]
        public async Task<IActionResult> Index(string FirstName, string LastName)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var result = await _userService.UpdateUserInfoAsync(userId, FirstName, LastName);

            if (result)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Помилка оновлення імені користувача.");
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditUsers(string id, string FirstName, string LastName)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                return BadRequest("Некоректні дані");
            }
            var success = await _userService.UpdateUserInfoAsync(id, FirstName, LastName);

            if (success)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Помилка оновлення імені користувача.");
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(string id, string role)
        {

            var selectedRoleId = Request.Form["RoleId"];

            role = await _userService.GetRoleNameById(selectedRoleId);





            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(role))
            {
                return BadRequest("Некоректні дані");
            }
            var success = await _userService.AssignRoleAsync(id, role);

            if (success)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return NotFound("Неможливо змінити роль для цього користувача");
            }
        }
    }
}
