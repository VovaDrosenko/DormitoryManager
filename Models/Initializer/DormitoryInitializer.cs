using DormitoryManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManager.Models.Initializer
{
    internal static class DormitoryInitializer
    {
        public static void SeedDormitories(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dormitory>().HasData(new Dormitory
            {
                DormitoryID = 1,
                DormitoryNumber = 7
            });
        }
    }
}
