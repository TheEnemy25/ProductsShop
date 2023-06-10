using Microsoft.AspNetCore.Identity;
using ProductsShop.Constants;
using ProductsShop.Models;

namespace ProductsShop.Data.Context
{
    public class DBSeeder
    {
        public static async Task SeederDefaultData(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            //ADD roles to db
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            //Create admin user

            var admin = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            };
            var userInDB = await userManager.FindByEmailAsync(admin.Email);
            if (userInDB is null)
            {
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
    }
}
