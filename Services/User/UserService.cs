
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryManager.Models.DTO_s.User;
using DormitoryManager.Models.Entities;

namespace DormitoryManager.Services.User
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public UserService(RoleManager<IdentityRole> roleManager, IConfiguration config, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> LoginUserAsync(LoginUserDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User or password incorrect."
                };
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, model.RememberMe);
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User signed in successfully."
                };
            }

            if (result.IsNotAllowed)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Confirm your email please."
                };
            }

            if (result.IsLockedOut)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User is locked out. Connect with site administrator."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "User or password incorrect."
            };
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            List<UsersDto> mappedUsers = users.Select(u => _mapper.Map<AppUser, UsersDto>(u)).ToList();

            for (int i = 0; i < users.Count; i++)
            {
                mappedUsers[i].Role = (await _userManager.GetRolesAsync(users[i])).FirstOrDefault();
            }


            return new ServiceResponse
            {
                Success = true,
                Message = "All users loaded.",
                Payload = mappedUsers
            };
        }

        public async Task<ServiceResponse> ChangePasswordAsync(ChangePasswordDto model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Message = "User not found.",
                    Success = false
                };
            }

            if (model.Password != model.ConfirmPassword)
            {
                return new ServiceResponse
                {
                    Message = "Password do not match.",
                    Success = false
                };
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Message = "Password successfully updated.",
                    Success = true,
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Message = "Password not updated.",
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description),
                };
            }
        }


        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User successfullt deleted."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Sonething wring. Connect with your admin.",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<List<IdentityRole>> LoadRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<bool> UpdateUserInfoAsync(string id, string newFirstName, string newLastName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {

                return false;
            }
            user.FirstName = newFirstName;
            user.LastName = newLastName;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> AssignRoleAsync(string id, string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);



            if (!roleExists)
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName);
            return addToRoleResult.Succeeded;
        }
        public async Task<string> GetRoleNameById(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role != null)
            {
                return role.Name;
            }

            // Якщо роль не знайдено, повертаємо порожню строку або інший відповідний значення за замовчуванням
            return string.Empty;
        }

        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ServiceResponse { Success = false, Message = "User not found." };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var mappedUser = _mapper.Map<AppUser, EditUserDto>(user);
            mappedUser.Role = roles[0];

            return new ServiceResponse
            {
                Success = true,
                Message = "User loaded!",
                Payload = mappedUser
            };
        }

        public async Task<ServiceResponse> CreateAsync(CreateUserDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return new ServiceResponse
                {
                    Message = "User exists.",
                    Success = false,
                };
            }

            var mappedUser = _mapper.Map<CreateUserDto, AppUser>(model);
            IdentityResult result = await _userManager.CreateAsync(mappedUser, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(mappedUser, model.Role);

                return new ServiceResponse
                {
                    Message = "User successfully created.",
                    Success = true,
                };
            }

            List<IdentityError> errorList = result.Errors.ToList();

            string errors = "";
            foreach (var error in errorList)
            {
                errors = errors + error.Description.ToString();
            }

            return new ServiceResponse
            {
                Message = "User creating error.",
                Success = false,
                Payload = errors
            };

        }
    }
}
