using DormitoryManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManager.Models.Initializer {
    internal static class FacultyInitializer {
        public static void SeedFaculties(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<Faculty>().HasData(new Faculty {
                FacultyId = "0",
                FacultyName = "FIT",
                FacultyAddress = "15"
            });
        }
    }
}
