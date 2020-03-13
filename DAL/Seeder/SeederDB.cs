using DAL.Entities;
using DAL.IdentityModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Seeder
{
    public class SeederDB
    {
        public static void SeedUsers(UserManager<DbUser> userManager,
            EFDbContext _context){
            var count = userManager.Users.Count();
            if (count <= 0)
            {
                string email = "losha.pisnyak42@gmail.com";
                var roleName = "Admin";
                var user = new DbUser
                {
                    Email = email,
                    UserName = "Karon",
                    PhoneNumber = "+38(067)855-22-65"                   
                };
                var result = userManager.CreateAsync(user, "Qwerty1_").Result;
                var userprofile = new UserProfile
                {
                    Id = user.Id,
                    Name = "Karonuk",
                    RegistrationDate = DateTime.Now,
                    User=user                    
                };
                _context.Users.Add(userprofile);               
                _context.SaveChanges();
                result = userManager.AddToRoleAsync(user, roleName).Result;            }
        }

        public static void SeedRoles(RoleManager<DbRole> roleManager)
        {
            var count = roleManager.Roles.Count();
            if (count <= 0)
            {
                var roleName = "User";
                var result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
                roleName = "Admin";
                result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
            }
        }

        public static void SeedData(IServiceProvider services, IHostingEnvironment env, IConfiguration config)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                var managerUser = scope.ServiceProvider.GetRequiredService < UserManager<DbUser>>();
                var context = scope.ServiceProvider.GetRequiredService<EFDbContext>();               
                SeederDB.SeedRoles(managerRole);
                SeederDB.SeedUsers(managerUser,context);
            }
        }
    }
}
