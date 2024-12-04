using Company.G05.BLL;
using Company.G05.BLL.Interface;
using Company.G05.BLL.Repositories;
using Company.G05.DAL.Data.Context;
using Company.G05.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVC_03.Company.G05.PL.Services;
using MVC_03.Controllers;
using MVC_03.Mapping;
using MVC_03.Services;
using System.Reflection;

namespace MVC_03
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<AppDbContext>(); // Allow DI For AppDbContext
            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); // Allow DI Using Extension Method
            builder.Services.AddScoped<IDepartmentRepository,DepartmentReposirtory>();
            builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();

            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            // builder.Services.AddScoped<UserManager<ApplicationUser>>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.AddScoped<IScopedServices,ScopedServices>(); // Per Request
            builder.Services.AddTransient<ITransientServices,TransientServices>(); // Per Operation
            builder.Services.AddSingleton<ISingeltonServices,SingeltonServices>(); // Per Application

            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            //builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            //builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            });

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

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
