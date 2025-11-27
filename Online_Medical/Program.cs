using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Medical.ALL_DATA;
using Online_Medical.Interface;
using Online_Medical.Models;
using Online_Medical.Repository;
using Online_Medical.EServices;
namespace Online_Medical
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Database Configuration
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(connectionString));
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
               
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
            })
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders(); 


            // 2. Add Session Service (Important for Login)
            builder.Services.AddDistributedMemoryCache(); // Stores session in memory
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session dies after 30 mins
                options.Cookie.HttpOnly = true; // Security: Protects cookie
                options.Cookie.IsEssential = true; // Required for the app to work
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IAppointment,AppointmentReporsitory>();


            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await Online_Medical.Repository.DbInitializer.Initialize(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // 3. Enable Session Middleware (Must be before Authorization)
            app.UseSession();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}