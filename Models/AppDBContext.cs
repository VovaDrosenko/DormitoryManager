using DormitoryManager.Models.Entities;
using DormitoryManager.Models.Initializer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace DormitoryManager.Models
{
    public class AppDBContext : IdentityDbContext
    {
        public AppDBContext() : base() { }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<Dormitory> Dormitories { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedDormitories();
            modelBuilder.SeedRooms();
            modelBuilder.SeedUsers();


            modelBuilder.Entity<Dormitory>()
                .HasKey(r => r.DormitoryID);
            modelBuilder.Entity<Room>()
                .HasKey(r => r.RoomID);
            modelBuilder.Entity<User>()
                .HasKey(r => r.UserID);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Dormitory)
                .WithMany(r => r.Rooms)
                .HasForeignKey(r => r.DormitoryID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Room)
                .WithMany()
                .HasForeignKey(u => u.RoomID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

