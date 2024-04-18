﻿using DormitoryManager.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace DormitoryManager.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly DormitoryManagerContext _context;
        private readonly IMapper _mapper;

        public AuthorizationController(DormitoryManagerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Authenticate user from database
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    // Redirect authenticated user to a dashboard or home page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }
            }
            return View(model);
        }

        // Other actions like Register, Logout, etc. can be implemented similarly
    }
}