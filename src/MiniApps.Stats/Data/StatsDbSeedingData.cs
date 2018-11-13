using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MiniApps.Stats.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApps.Stats.Data
{
    public static class StatsDbSeedingData
    {

        public static async Task InitializeDataAsync(StatsDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Administrator", "Staff", "Merchant" };
            string password = "P@ssw0rd";

            // 添加角色
            IdentityResult result;
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    result = await roleManager.CreateAsync(new IdentityRole(roleName));                    
                }
            }

            // 添加管理员账户
            if(await userManager.FindByNameAsync("administrator") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "administrator",
                    Email = "administrator@gmail.com",
                    PhoneNumber = "1234567890"
                };
                result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, roleNames[0]);
                }
            }
        } 
    }
}
