using DormitoryManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManager.Models.Initializer
{
    internal static class UserInitializer
    {
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                UserID = 1,
                UserFirstName = "Volodymyr",
                UserLastName = "Drosenko",
                UserMiddleName = "Igorovich",
                DormitoryID = 1,
                RoomID = 1 
            });
        }
    }
}
