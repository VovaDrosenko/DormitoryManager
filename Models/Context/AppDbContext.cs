using DormitoryManager.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManager.Models.Context
{
    internal class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Dormitory> Dormitories { get; set; }
        public DbSet<DormitoryComendant> DormitoryComendants { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<FacultyWorker> FacultyWorkers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Student> Students { get; set; }
        //public DbSet<StudentRoom> StudentRooms { get; set; }
        public DbSet<Worker> Workers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-one relationship between DormitoryComendant and Worker
            modelBuilder.Entity<DormitoryComendant>()
                .HasOne(dc => dc.Worker)
                .WithOne(w => w.DormitoryComendant)
                .HasForeignKey<DormitoryComendant>(dc => dc.WorkerId);


            modelBuilder.Entity<FacultyWorker>()
                .HasOne(fw => fw.Worker)
                .WithOne(w => w.FacultyWorker)
                .HasForeignKey<FacultyWorker>(fw => fw.WorkerId);
        }
    }
}
