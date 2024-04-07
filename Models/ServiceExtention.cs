using Microsoft.EntityFrameworkCore;
using System;

namespace DormitoryManager.Models
{
    public static class ServiceExtention
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDBContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

        }
    }
}
