using Hangfire;
using Microsoft.EntityFrameworkCore;
using RingoMedia.App.Validation;
using RingoMedia.DataAccess;
using RingoMedia.Infrastructure.Interfaces;
using RingoMedia.Infrastructure.Repositories;
using RingoMedia.Service.Services;
using RingoMedia.UI;
using RingoMedia.UI.Services;

var builder = WebApplication.CreateBuilder(args);



// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register DbContext 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableDetailedErrors().LogTo(Console.WriteLine));

// Register generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register specific repositories
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IReminderRepository, ReminderRepository>();

// Register services
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<ReminderService>();

// Register app services
builder.Services.AddScoped<DepartmentAppService>();
builder.Services.AddScoped<ReminderAppService>();

// Register generic services 
builder.Services.AddScoped<UploadService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<SendReminderEmailJob>();

// hangfire  
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddHangfire(options => options.UseSqlServerStorage(connectionString));
builder.Services.AddHangfireServer();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
// hangfire 
app.UseHangfireDashboard();
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

app.Run();
