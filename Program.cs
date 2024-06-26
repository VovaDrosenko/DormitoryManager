using DormitoryManager;
using DormitoryManager.Models.Context;
using DormitoryManager.Models.Initializers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Create connection string
string connStr = builder.Configuration.GetConnectionString("DormitoryContext");

// Database context
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connStr));

// Add Core services
builder.Services.AddCoreServices();

builder.Services.AddRepositories();

// Add Infrastructure Service
builder.Services.AddInfrastructureService();

// Add Mapping
builder.Services.AddMapping();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await UsersAndRolesInitializer.SeedUsersAndRoles(app);
app.Run();
