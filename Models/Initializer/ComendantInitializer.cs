using DormitoryManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManager.Models.Initializer {
    internal static class ComendantInitializer {
        public static void SeedComendants(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<Comendant>().HasData(new Comendant {
                ComendantId = "0",
                ComendantName = "Savelka"
            });
        }
    }
}
