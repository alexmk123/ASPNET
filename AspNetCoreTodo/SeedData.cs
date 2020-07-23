using System;
using System.Threading.Tasks;
using System.Linq;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreTodo
{
    public static class SeedData
    {
        public static async Task InitializeAsync(
            IServiceProvider services)
            {
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await EnsureRoleAsync(roleManager);

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await EnsureTestAdminAsync(userManager);
            }
        
        private static async Task EnsureRoleAsync(
            RoleManager<IdentityRole> roleManager)
            {
                var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);
                if (alreadyExists) return;
                await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
            }

        private static async Task EnsureTestAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var testeAdmin = await userManager.Users.Where
            (x => x.UserName == "alibaba@alibaba.com").SingleOrDefaultAsync();

            if(testeAdmin != null) 
            {
            Console.WriteLine ("usuario ja existe");
            Console.WriteLine ("Confirmado ?"+testeAdmin.EmailConfirmed);
            
            return;
            }
            testeAdmin = new ApplicationUser
            {
                UserName = "admin@todo.local",
                Email = "admin@todo.local"
                
            };
            Console.WriteLine ("usuario criado");

            await userManager.CreateAsync(testeAdmin,"NotSecure123!!");
            await userManager.AddToRoleAsync(testeAdmin, Constants.AdministratorRole);
        }
    }
}