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

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<Dormitory> Dormitorys { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>();
            builder.Entity<Room>();
            builder.Entity<Dormitory>();
            builder.SeedDormitories();
            builder.SeedRooms();
            builder.SeedUsers();
            base.OnModelCreating(builder);

        }
    }
}
