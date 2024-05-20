using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using DormitoryManager.Models.DTO_s.User;
using DormitoryManager.Services;
using DormitoryManager.Valudation.User;
using DormitoryManager.Validation.User;
using DormitoryManager.Interfaces;
using DormitoryManager.Models.DTO_s.Student;
using DormitoryManager.Models.DTO_s.Faculty;
using DormitoryManager.Validation.Faculty;
using Microsoft.IdentityModel.Tokens;
using DormitoryManager.Models.Entities;
using AutoMapper;
using DormitoryManager.Models.DTO_s.Dormitory;
using FluentValidation;
using DormitoryManager.Validation.Dormitory;
using DormitoryManager.ViewModel;
using DormitoryManager.Models.DTO_s.Room;
using DormitoryManager.Validation.Room;

namespace DormitoryManager.Controllers
{

    [Authorize]
    public class DashboardController : Controller {
        private readonly UserService _userService;
        private readonly IStudentService _studentService;
        private readonly IFacultyService _facultyService;
        private readonly IDormitoryService _dormService;
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService;



        public DashboardController(UserService userService, IStudentService studentService, IFacultyService facultyService, IDormitoryService dormitoryService, IRoomService roomService) {
            _userService = userService;
            _studentService = studentService;
            _facultyService = facultyService;
            _dormService = dormitoryService;
            _roomService = roomService;
        }

        public IActionResult Index() {
            return View();
        }

        [AllowAnonymous] // Method GET
        public async Task<IActionResult> SignIn() {
            var user = HttpContext.User.Identity.IsAuthenticated;
            if (user) {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        [AllowAnonymous] // Method POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginUserDto model) {
            var validator = new LoginUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid) {
                ServiceResponse result = await _userService.LoginUserAsync(model);
                if (result.Success) {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.AuthError = result.Message;
                return View(model);
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View(model);
        }

        public async Task<IActionResult> GetAll() {
            var result = await _studentService.GettAllSettStud();
            return View(result);
        }
        public async Task<IActionResult> Requests() {
            var students = await _studentService.GetAllRequest();
            // Assuming _facultyRepository is your repository for faculties
            var faculties = await _facultyService.GettAll();
            var result = from s in students join f in faculties on s.FacultyId equals f.Id
                         select new StudentsDto {
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
        public async Task<IActionResult> Logout() {
            await _userService.LogoutUserAsync();
            return RedirectToAction(nameof(SignIn));
        }



        public async Task<IActionResult> Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDto model) {
            var validator = new CreateUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid) {
                var result = await _userService.CreateAsync(model);
                if (result.Success) {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.AuthError = result.Payload;
                return View(model);
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View(model);
        }

        public async Task<IActionResult> Delete(int Id) {
            await _studentService.Delete(Id);

            return RedirectToAction(nameof(Requests));

        }


        public async Task<IActionResult> DeleteById(string id) {
            var result = await _userService.DeleteAsync(id);
            if (result.Success) {
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
            var student = await _studentService.Get(id);
            var faculties = await _facultyService.GettAll();
            var studentFaculty = faculties.FirstOrDefault(f => f.Id == student.FacultyId);

            ViewBag.Dormitories = await _dormService.GettAll();
            if (studentFaculty != null)
            {
                student.FacultyName = studentFaculty.FacultyName;
            }
            
            await GetRoles();
            if (student != null)
            {
                var base64Photo = ConvertToBase64(student.Photo);
                var base64Doc = ConvertToBase64(student.ApplicationScan);
                student.PhotoString = base64Photo;
                student.ApplicationScanString = base64Doc;
                return View(student);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomsByDormitory(int dormitoryId)
        {
            var rooms = await _roomService.GettAllInDorm(dormitoryId);
            ViewBag.Rooms = rooms;
            return Json(rooms);
        }

        private string ConvertToBase64(IFormFile file) {
            if (file == null) return null;

            using (MemoryStream ms = new MemoryStream()) {
                file.CopyTo(ms);
                byte[] fileBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(fileBytes);
                return "data:image/png;base64," + base64String;
            }
        }
        private async Task GetRoles() {
            var result = await _userService.LoadRoles();
            @ViewBag.RoleList = new SelectList(
          result, nameof(IdentityRole.Id),
              nameof(IdentityRole.Name)
              );
        }
        [HttpPost]
        public async Task<IActionResult> Index(string FirstName, string LastName) {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId)) {
                return NotFound();
            }

            var result = await _userService.UpdateUserInfoAsync(userId, FirstName, LastName);

            if (result) {
                return RedirectToAction("Index", "Dashboard");
            }
            else {
                ModelState.AddModelError("", "Помилка оновлення імені користувача.");
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditUsers(string id, string FirstName, string LastName) {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName)) {
                return BadRequest("Некоректні дані");
            }
            var success = await _userService.UpdateUserInfoAsync(id, FirstName, LastName);

            if (success) {
                return RedirectToAction("Index", "Dashboard");
            }
            else {
                ModelState.AddModelError("", "Помилка оновлення імені користувача.");
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(string id, string role) {

            var selectedRoleId = Request.Form["RoleId"];

            role = await _userService.GetRoleNameById(selectedRoleId);

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(role)) {
                return BadRequest("Некоректні дані");
            }
            var success = await _userService.AssignRoleAsync(id, role);

            if (success) {
                return RedirectToAction("Index", "Dashboard");
            }
            else {
                return NotFound("Неможливо змінити роль для цього користувача");
            }
        }
        #region Faculties
        public async Task<IActionResult> Faculties() {
            var faculties = await _facultyService.GettAll();
            return View(faculties.ToList());
        }

        public async Task<IActionResult> CreateFaculty() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFaculty(FacultiesDto model) {
            var validator = new CreateFacultyValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid) {
                await _facultyService.Create(model);
                return RedirectToAction(nameof(Faculties));
            }
            if (model.FacultyName?.Length < 3 || model.FacultyName.IsNullOrEmpty()) {
                ViewBag.NameError = "Назва факультету повинна включати 3 і більше символа\n";
            }
            if (model.FacultyAddress?.Length < 3 || model.FacultyAddress.IsNullOrEmpty()) {
                ViewBag.AddressError = "\nАдреса факультету повинна включати 3 і більше символа";
            }
            return View(model);
        }

        

        public async Task<IActionResult> EditFaculty(int Id) {
            var fac = await _facultyService.Get(Id);
            
            return View(fac);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFaculty(Faculty model) {
            await _facultyService.Update(model);
            return RedirectToAction(nameof(Faculties));
        }

        //var validator = new CreateUserValidation();
        //var validationResult = await validator.ValidateAsync(model);
        //    if (validationResult.IsValid) {
        //        var result = await _userService.CreateAsync(model);
        //        if (result.Success) {
        //            return RedirectToAction(nameof(Index));
        //  }
        //  ViewBag.AuthError = result.Payload;
        //        return View(model);
        //}
        //ViewBag.AuthError = validationResult.Errors[0];
        //return View(model);
        public async Task<IActionResult> DeleteFaculty(int Id) {
            await _facultyService.Delete(Id);
            return RedirectToAction(nameof(Faculties));

        }
        #endregion
        #region DORMITORIES
        public async Task<IActionResult> Dormitories() {
            var Dormitories = await _dormService.GettAll();
            return View(Dormitories.ToList());
        }

        public async Task<IActionResult> EditDormitory(int Id) {
            var dorm = await _dormService.Get(Id);

            return View(dorm);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDormitory(Dormitory model) {
            await _dormService.Update(model);
            return RedirectToAction(nameof(Dormitories));
        }
        //DormitoryDetails
        public async Task<IActionResult> DormitoryDetails(int Id) {
            var dorm = await _dormService.Get(Id);

            return View(dorm);

        }

        public async Task<IActionResult> DeleteDormitory(int Id) {
            await _dormService.Delete(Id);
            return RedirectToAction(nameof(Dormitories));
        }

        public async Task<IActionResult> CreateDormitory() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDormitory(DormitoryDto model) {
            var validator = new CreateDormitoryValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid) {
                await _dormService.Create(model);
                return RedirectToAction(nameof(Dormitories));
            }
            if (model.DormNumber?.Length < 1 || model.DormNumber.IsNullOrEmpty()) {
                ViewBag.NumberError = "Номер гуртожитку повинен включати більше чим 1 символ\n";
            }
            if (model.Address?.Length < 3 || model.Address.IsNullOrEmpty()) {
                ViewBag.AddressError = "\nАдреса Гуртожитку повинна включати 5 і більше символів";
            }
            if (model?.Floors < 1 || model?.Floors == null) {
                ViewBag.FloorsError = "\nКількість поверхів повинна бути більшою за 0";
            }
            return View(model);
        }

        #endregion
        #region ROOMS
        public async Task<IActionResult> Rooms(int dormId) {
            var rooms = await _roomService.GettAllInDorm(dormId);
            var roomViewModel = new RoomsViewModel { Rooms = rooms, dormitoryId = dormId, Room = new RoomDto() };
            return View(roomViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRoom(RoomsViewModel model) {
            var room = model.Room;
            var existingRoom = await _roomService.GetByNumberOfRoom(room.NumberOfRoom);
            var validator = new CreateRoomValidation();
            var res = await validator.ValidateAsync(model.Room);
            if (res.IsValid) {
                if(existingRoom.ResidentsGender != null) {
                    existingRoom.ResidentsGender = room.ResidentsGender;
                    existingRoom.NumberOfBeds = room.NumberOfBeds;
                    existingRoom.NumberOfRoom = room.NumberOfRoom;
                    await _roomService.Update(existingRoom);
                    return RedirectToAction(nameof(Rooms), new { dormId = room.DormId });
                }
                await _roomService.Create(room);
            }
            
            return RedirectToAction(nameof(Rooms), new {dormId = room.DormId});
        }

        public async Task<IActionResult> DeleteRoom(int Id) {
            var room = await _roomService.Get(Id);
            await _roomService.Delete(Id);
            return RedirectToAction(nameof(Rooms), new {dormId = room.DormId});
        }
        #endregion
    }
}
