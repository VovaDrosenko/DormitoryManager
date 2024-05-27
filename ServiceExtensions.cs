using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DormitoryManager.Models.Context;
using DormitoryManager.Models.Entities;
using DormitoryManager.Interfaces;
using DormitoryManager.Repository;
using DormitoryManager.Services;
using DormitoryManager.AutoMapper;

namespace DormitoryManager
{
    public static class ServiceExtensions
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<UserService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IDormitoryService, DormitoryService>();
            services.AddScoped<IRoomService, RoomService>();
        }

        public static void AddInfrastructureService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperUserProfile));
            services.AddAutoMapper(typeof(AutoMapperStudentProfile));
            services.AddAutoMapper(typeof(AutoMapperFacultyProfile));
            services.AddAutoMapper(typeof(AutoMapperDormitoryProfile));
            services.AddAutoMapper(typeof(AutoMapperRoomProfile));
        }
    }
}
