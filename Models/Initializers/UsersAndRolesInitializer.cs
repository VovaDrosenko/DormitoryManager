using Microsoft.AspNetCore.Identity;
using DormitoryManager.Models.Entities;
using DormitoryManager.Models.Context;

namespace DormitoryManager.Models.Initializers
{
    public static class UsersAndRolesInitializer
    {
        public static async Task SeedUsersAndRoles(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                if(userManager.FindByEmailAsync("admin@email.com").Result == null)
                {
                    AppUser admin  = new AppUser()
                    {
                        UserName = "admin@email.com",
                        FirstName = "John",
                        LastName = "Snow",
                        Email = "admin@email.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+xx(xxx)xxx-xx-xx",
                        PhoneNumberConfirmed = true,
                    };

                    context.Roles.AddRange(
                        new IdentityRole()
                        {
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new IdentityRole()
                        {
                            Name = "Dekanat",
                            NormalizedName = "DEKANAT"
                        },
                        new IdentityRole()
                        {
                            Name = "Komendant",
                            NormalizedName = "KOMENDANT"
                        },
                        new IdentityRole()
                        {
                            Name = "User",
                            NormalizedName = "USER"
                        });

                    await context.SaveChangesAsync();

                    IdentityResult adminResult = userManager.CreateAsync(admin, "Qwerty-1").Result;
                    if(adminResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(admin, "Administrator").Wait();
                    }
                }
            }
        }
    }
}
