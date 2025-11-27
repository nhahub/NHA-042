using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Medical.ALL_DATA;
using Online_Medical.Interface;
using Online_Medical.Mapping;
using Online_Medical.Models;
using Online_Medical.Repository;
using Online_Medical.Services;

namespace Online_Medical
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Database Configuration
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(connectionString));



            // 2. Add Session Service (Important for Login)
            builder.Services.AddDistributedMemoryCache(); // Stores session in memory
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session dies after 30 mins
                options.Cookie.HttpOnly = true; // Security: Protects cookie
                options.Cookie.IsEssential = true; // Required for the app to work
            });



            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            // Add services to the container.
            builder.Services.AddControllersWithViews();



            // Repositorie
            //builder.Services.AddScoped<IRepository<Appointment,int>,AppointmentReporsitory>();
            builder.Services.AddScoped<IRepository<Doctor,string>, DoctorRepository>();
            builder.Services.AddScoped<IRepository<Specialization,int>, SpecializationRepository>();




            // Services
            builder.Services.AddScoped<DoctorService>();

            // Auto Mapper Configurations
            builder.Services.AddAutoMapper(typeof(DoctorMappingProfile));




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();   
            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}