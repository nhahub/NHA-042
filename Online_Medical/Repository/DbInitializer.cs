using Microsoft.AspNetCore.Identity;
using Online_Medical.Models;

namespace Online_Medical.Repository
 
{
    public class DbInitializer
    {
        public static async Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
           
            string[] roleNames = { "Admin", "Doctor", "Patient" };

           
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

           
            var adminUser = await userManager.FindByEmailAsync("admin@system.com");

            if (adminUser == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@system.com",
                    Email = "admin@system.com",
                    FirstName = "System",    
                    LastName = "Admin",      
                    EmailConfirmed = true
                };

                
                var result = await userManager.CreateAsync(admin, "Admin@123");

                if (result.Succeeded)
                {
                    
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }

    }
}
