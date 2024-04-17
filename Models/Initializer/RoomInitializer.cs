using DormitoryManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManager.Models.Initializer
{
    internal static class RoomInitializer
    {
        public static void SeedRooms(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(new Room
            {
               
            });
        }
    }
}
