using DormitoryManager.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;

namespace DormitoryManager.Models.Context
{
    internal class AppDbContext : IdentityDbContext
    {
        public AppDbContext() : base()
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<AppUser> Users { get; set; }
        public  DbSet<Dormitory> Dormitories { get; set; }

        public  DbSet<DormitoryComendant> DormitoryComendants { get; set; }

        public  DbSet<Faculty> Faculties { get; set; }

        public  DbSet<FacultyWorker> FacultyWorkers { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentRoom> StudentRooms { get; set; }

        public DbSet<Worker> Workers { get; set; }
    }
}
