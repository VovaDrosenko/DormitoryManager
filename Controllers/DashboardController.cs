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
using DormitoryManager.Validation.Dormitory;
using DormitoryManager.ViewModel;
using DormitoryManager.Models.DTO_s.Room;
using DormitoryManager.Validation.Room;
using DormitoryManager.Validation.Student;
using DormitoryManager.Models.DTO_s.StudentRoom;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DormitoryManager.Controllers
{

    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserService _userService;
        private readonly IStudentService _studentService;
        private readonly IFacultyService _facultyService;
        private readonly IDormitoryService _dormService;
        private readonly IStudentRoomService _studentRoomService;
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService;
        private readonly UserManager<AppUser> _userManager;



        public DashboardController(UserManager<AppUser> userManager, UserService userService, IStudentService studentService, IFacultyService facultyService, IDormitoryService dormitoryService, IRoomService roomService, IStudentRoomService studentRoomService = null)
        {
            _userService = userService;
            _studentService = studentService;
            _facultyService = facultyService;
            _dormService = dormitoryService;
            _roomService = roomService;
            _studentRoomService = studentRoomService;
            _userManager = userManager;
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
            var std = await _studentService.GettAllSettStud();
            var faculties = await _facultyService.GettAll();
            var result = from s in std
                         join f in faculties on s.FacultyId equals f.Id
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
        public async Task<IActionResult> Requests()
        {
            var students = await _studentService.GetAllRequest();
            // Assuming _facultyRepository is your repository for faculties
            var faculties = await _facultyService.GettAll();
            var result = from s in students
                         join f in faculties on s.FacultyId equals f.Id
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

        [HttpGet]
        public async Task<IActionResult> InProgress()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Retrieve data from services
            var students = await _studentService.GetAllInProgress();
            var faculties = await _facultyService.GettAll();

            // Create a dictionary for faculties to optimize lookups
            var facultyDict = faculties.ToDictionary(f => f.Id);

            // Prepare the result list
            var result = new List<StudentsDto>();

            foreach (var student in students)
            {
                var faculty = facultyDict[student.FacultyId];
                var room = await _roomService.Get(student.RoomId);
                var dorm = await _dormService.Get(room.DormId);

                result.Add(new StudentsDto
                {
                    Id = student.Id,
                    StudentLastname = student.StudentLastname,
                    StudentMiddlename = student.StudentMiddlename,
                    StudentName = student.StudentName,
                    StudentPhone = student.StudentPhone,
                    FacultyId = student.FacultyId,
                    Gender = student.Gender,
                    FacultyName = faculty.FacultyName,
                    RoomId = student.RoomId,
                    RoomNum = room.NumberOfRoom,
                    Room = room,
                    DormitoryId = dorm.Id,
                    DormNum = dorm.DormNumber

                });
            }

            // Filter results by DormId
            var filteredResult = result.Where(s => s.DormitoryId == Convert.ToInt32(user.DormId)).ToList();

            return View(filteredResult);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutUserAsync();
            return RedirectToAction(nameof(SignIn));
        }


        #region

        public async Task<IActionResult> Users()
        {
            var model = new UserDetailsViewModel();
            model.users = await _userManager.Users.ToListAsync();
            model.dormitory = await _dormService.GettAll();
            model.faculties = await _facultyService.GettAll();
            return View(model);
        }
        public async Task<IActionResult> DeleteUser(AppUser model)
        {
            await _userService.DeleteAsync(model.Id);
            return RedirectToAction(nameof(Users));
        }

        public async Task<IActionResult> Create()
        {
            var model = new UserViewModel();
            model.dormitory = await _dormService.GettAll();
            model.faculties = await _facultyService.GettAll();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            var validator = new CreateUserValidation();
            var validationResult = await validator.ValidateAsync(model.user);
            if (validationResult.IsValid)
            {
                if (model.user.Role == "Dekanat")
                    model.user.DormId = null;
                else if (model.user.Role == "Komendant")
                    model.user.FacultyId = null;
                else
                {
                    model.user.FacultyId = null;
                    model.user.DormId = null;
                }
                var result = await _userService.CreateAsync(model.user);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.AuthError = result.Payload;
                return View(model);
            }
            ViewBag.AuthError = validationResult.Errors[0];
            model.dormitory = await _dormService.GettAll();
            model.faculties = await _facultyService.GettAll();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var result = await _userService.GetByIdAsync(id);
            await GetRoles();
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View();
        }
        #endregion

        public async Task<IActionResult> Delete(int Id)
        {
            await _studentService.Delete(Id);

            return RedirectToAction(nameof(Requests));

        }

        public async Task<IActionResult> DeleteFromSettl(int Id)
        {
            await _studentService.Delete(Id);

            return RedirectToAction(nameof(GetAll));

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


        public async Task<IActionResult> GetAllRooms()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var rooms = await _roomService.GettAllInDorm(Convert.ToInt32(user.DormId));
            foreach (var room in rooms)
            {
                room.FreeBeds = await _studentService.GetOccupiedBedsCount(room.Id);
            }
            var sortedRooms = rooms.OrderBy(r => r.NumberOfRoom).ToList();

            return View(sortedRooms);

        }

        [HttpGet]
        public async Task<IActionResult> StudentsInRoom(int id, string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            ViewBag.ReturnUrl = returnUrl;
            var room = await _roomService.Get(id);
            var students = await _studentService.GettAllSettStud();
            var faculties = await _facultyService.GettAll();
            ViewBag.Room = room.NumberOfRoom;

            var filteredResult = students.Where(s => s.RoomId == id).ToList();
            foreach (var std in filteredResult)
            {
                var fac = await _facultyService.Get(std.FacultyId);
                std.FacultyName = fac.FacultyName;
            }


            return View(filteredResult);
        }


        [HttpGet]
        public async Task<IActionResult> GetSettlStudent(int id)
        {
            var student = await _studentService.Get(id);
            var faculty = await _facultyService.Get(student.FacultyId);
            var room = await _roomService.Get(student.RoomId);
            var dorm = await _dormService.Get(room.DormId);
            student.FacultyName = faculty.FacultyName;
            student.DormNum = dorm.DormNumber;
            student.RoomNum = room.NumberOfRoom;



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
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await _studentService.Get(id);
            var faculties = await _facultyService.GettAll();
            var studentFaculty = faculties.FirstOrDefault(f => f.Id == student.FacultyId);

            ViewBag.Dormitories = await _dormService.GettAll();
            ViewBag.StudentGender = student.Gender;

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

        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentsDto studentsDto)
        {
            studentsDto.ApplicationScan = ConvertFromBase64String(studentsDto.ApplicationScanString);
            studentsDto.Photo = ConvertFromBase64String(studentsDto.PhotoString);
            var validator = new UpdateStudentsValidation();
            var validationResult = await validator.ValidateAsync(studentsDto);
            if (validationResult.IsValid)
            {

                await _studentService.Update(studentsDto);

                return RedirectToAction(nameof(Requests));
            }

            ViewBag.AuthError = validationResult.Errors[0];
            return View(studentsDto);

        }

        [HttpGet]
        public async Task<IActionResult> EditStudentComendant(int id, string returnUrl = null)
        {
            var student = await _studentService.Get(id);
            var faculties = await _facultyService.GettAll();
            var studentFaculty = faculties.FirstOrDefault(f => f.Id == student.FacultyId);
            var room = await _roomService.Get(student.RoomId);
            var dorm = await _dormService.Get(room.DormId);

            student.DormNum = dorm.DormNumber;
            student.DormitoryId = dorm.Id;
            student.RoomId = room.Id;
            student.RoomNum = room.NumberOfRoom;
            ViewBag.StudentGender = student.Gender;
            ViewBag.ReturnUrl = returnUrl;

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

        [HttpPost]
        public async Task<IActionResult> EditStudentComendant(StudentsDto studentsDto, string returnUrl = null)
        {
            studentsDto.ApplicationScan = ConvertFromBase64String(studentsDto.ApplicationScanString);
            studentsDto.Photo = ConvertFromBase64String(studentsDto.PhotoString);

            var validator = new UpdateStudentsValidation();
            var validationResult = await validator.ValidateAsync(studentsDto);

            if (validationResult.IsValid)
            {
                await _studentService.Update(studentsDto);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction(nameof(InProgress));
            }

            ViewBag.AuthError = validationResult.Errors[0];
            return View(studentsDto);
        }


        [HttpGet]
        public IActionResult ReturnBack(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> GetRoomsByDormitoryAndGender(int dormitoryId, string gender)
        {
            var rooms = await _roomService.GetByDormitoryIdAndGender(dormitoryId, gender);
            return Json(rooms);
        }

        private string ConvertToBase64(byte[] file)
        {
            if (file == null) return null;

            using (MemoryStream ms = new MemoryStream())
            {
                string base64String = Convert.ToBase64String(file);
                return "data:image/png;base64," + base64String;
            }
        }

        private byte[] ConvertFromBase64String(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return null;

            var data = base64String.Substring(base64String.IndexOf(",") + 1);
            return Convert.FromBase64String(data);
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
        #region Faculties
        public async Task<IActionResult> Faculties()
        {
            var faculties = await _facultyService.GettAll();
            return View(faculties.ToList());
        }

        public async Task<IActionResult> CreateFaculty()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFaculty(FacultiesDto model)
        {
            var validator = new CreateFacultyValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _facultyService.Create(model);
                return RedirectToAction(nameof(Faculties));
            }
            if (model.FacultyName?.Length < 3 || model.FacultyName.IsNullOrEmpty())
            {
                ViewBag.NameError = "Назва факультету повинна включати 3 і більше символа\n";
            }
            if (model.FacultyAddress?.Length < 3 || model.FacultyAddress.IsNullOrEmpty())
            {
                ViewBag.AddressError = "\nАдреса факультету повинна включати 3 і більше символа";
            }
            return View(model);
        }



        public async Task<IActionResult> EditFaculty(int Id)
        {
            var fac = await _facultyService.Get(Id);

            return View(fac);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFaculty(Faculty model)
        {
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
        public async Task<IActionResult> DeleteFaculty(int Id)
        {
            await _facultyService.Delete(Id);
            return RedirectToAction(nameof(Faculties));

        }
        #endregion
        #region DORMITORIES
        public async Task<IActionResult> Dormitories()
        {
            var Dormitories = await _dormService.GettAll();
            return View(Dormitories.ToList());
        }

        public async Task<IActionResult> EditDormitory(int Id)
        {
            var dorm = await _dormService.Get(Id);

            return View(dorm);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDormitory(Dormitory model)
        {
            await _dormService.Update(model);
            return RedirectToAction(nameof(Dormitories));
        }
        //DormitoryDetails
        public async Task<IActionResult> DormitoryDetails(int Id)
        {
            var dorm = await _dormService.Get(Id);

            return View(dorm);

        }

        public async Task<IActionResult> DeleteDormitory(int Id)
        {
            await _dormService.Delete(Id);
            return RedirectToAction(nameof(Dormitories));
        }

        public async Task<IActionResult> CreateDormitory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDormitory(DormitoryDto model)
        {
            var validator = new CreateDormitoryValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _dormService.Create(model);
                return RedirectToAction(nameof(Dormitories));
            }
            if (model.DormNumber?.Length < 1 || model.DormNumber.IsNullOrEmpty())
            {
                ViewBag.NumberError = "Номер гуртожитку повинен включати більше чим 1 символ\n";
            }
            if (model.Address?.Length < 3 || model.Address.IsNullOrEmpty())
            {
                ViewBag.AddressError = "\nАдреса Гуртожитку повинна включати 5 і більше символів";
            }
            if (model?.Floors < 1 || model?.Floors == null)
            {
                ViewBag.FloorsError = "\nКількість поверхів повинна бути більшою за 0";
            }
            return View(model);
        }

        #endregion
        #region ROOMS
        public async Task<IActionResult> Rooms(int dormId)
        {
            var rooms = await _roomService.GettAllInDorm(dormId);
            var roomViewModel = new RoomsViewModel { Rooms = rooms, dormitoryId = dormId, Room = new RoomDto() };
            return View(roomViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRoom(RoomsViewModel model)
        {
            var room = model.Room;
            var existingRoom = await _roomService.GetByNumberOfRoom(room.NumberOfRoom, room.DormId);
            var validator = new CreateRoomValidation();
            var res = await validator.ValidateAsync(model.Room);
            if (res.IsValid)
            {
                if (existingRoom.NumberOfRoom == room.NumberOfRoom)
                {
                    existingRoom.ResidentsGender = room.ResidentsGender;
                    existingRoom.NumberOfBeds = room.NumberOfBeds;
                    existingRoom.NumberOfRoom = room.NumberOfRoom;
                    existingRoom.Floor = room.Floor;
                    await _roomService.Update(existingRoom);
                    return RedirectToAction(nameof(Rooms), new { dormId = room.DormId });
                }
                room.FreeBeds = Int32.Parse(room.NumberOfBeds);
                await _roomService.Create(room);
            }

            return RedirectToAction(nameof(Rooms), new { dormId = room.DormId });
        }

        public async Task<IActionResult> DeleteRoom(int Id)
        {
            var room = await _roomService.Get(Id);
            await _roomService.Delete(Id);
            return RedirectToAction(nameof(Rooms), new { dormId = room.DormId });
        }
        #endregion
        #region REPORTS
        public async Task<IActionResult> reports()
        {

            return View();
        }

        public async Task<IActionResult> FreeRoomReports()
        {
            var rooms = await _roomService.GettAll();
            rooms = rooms.OrderBy(r => r.Floor).ThenBy(r => r.NumberOfRoom).ToList();
            var dorms = await _dormService.GettAll();
            var floors = rooms.Select(r => r.Floor).Distinct().OrderBy(f => f).ToList();
            var freeBedsPerFloor = rooms.GroupBy(r => r.Floor)
                             .Select(g => new { Floor = g.Key, FreeBeds = g.Sum(r => r.FreeBeds) })
                             .ToDictionary(x => x.Floor, x => (int?)x.FreeBeds);

            var roomViewModel = new RoomsViewModel
            {
                Rooms = rooms,
                Room = new RoomDto(),
                Dormitories = dorms,
                Floors = floors,
                FreeBedsPerFloor = freeBedsPerFloor
            };
            return View(roomViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FreeRoomReports(int dormId, int floor)
        {
            var roomsOnFloor = await _roomService.GettAllInDormAndFloor(dormId, floor);
            var rooms = await _roomService.GetAllByDormId(dormId);

            roomsOnFloor = roomsOnFloor.OrderBy(r => r.Floor).ThenBy(r => r.NumberOfRoom).ToList();

            var dorms = await _dormService.GettAll();

            // Отримання унікальних значень поверхів для всіх кімнат гуртожитка
            var allFloors = rooms.Select(r => r.Floor).Distinct().OrderBy(f => f).ToList();

            var freeBedsPerFloor = roomsOnFloor.GroupBy(r => r.Floor)
                .Select(g => new { Floor = g.Key, FreeBeds = g.Sum(r => r.FreeBeds) })
                .ToDictionary(x => x.Floor, x => x.FreeBeds);

            var roomViewModel = new RoomsViewModel
            {
                Rooms = roomsOnFloor,
                Room = new RoomDto(),
                Dormitories = dorms,
                Floors = allFloors,
                dormitoryId = dormId,
                SelectedFloor = floor,
                FreeBedsPerFloor = freeBedsPerFloor
            };

            return View(roomViewModel);
        }
        public FileResult ExportToExcell(string htmlTable)
        {
            return File(Encoding.UTF8.GetBytes(htmlTable), "application/vnd.ms-excel.sheet.3", "htmltable.xls");
        }
    }
    #endregion
}

