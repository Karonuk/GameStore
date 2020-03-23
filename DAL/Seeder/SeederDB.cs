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
            EFDbContext _context)
        {
            var count = userManager.Users.Count();
            if (count <= 0)
            {
                string email = "losha.pisnyak42@gmail.com";
                var roleName = "Admin";
                var user1 = new DbUser
                {
                    Email = email,
                    UserName = "Karon",
                    PhoneNumber = "+38(067)855-22-65"
                };
                var result = userManager.CreateAsync(user1, "Qwerty1_").Result;
                var userprofile1 = new UserProfile
                {
                    Id = user1.Id,
                    Name = "Karonuk",
                    RegistrationDate = DateTime.Now,
                    User = user1
                };
                _context.Users.Add(userprofile1);
                result = userManager.AddToRoleAsync(user1, roleName).Result;
                _context.SaveChanges();
                email = "paradoxInt@gmail.com";
                roleName = "User";
                var user2 = new DbUser
                {
                    Email = email,
                    UserName = "Paradox",
                    PhoneNumber = "+3706565433"
                };
                var result2 = userManager.CreateAsync(user2, "Qwerty1_").Result;
                var userProfile2 = new UserProfile
                {
                    Id = user2.Id,
                    Name = "ParadoxDev",
                    RegistrationDate = DateTime.Now,
                    User = user2
                };
                _context.Users.Add(userProfile2);
                result2 = userManager.AddToRoleAsync(user2, roleName).Result;
                _context.SaveChanges();
            }
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
        public static void SeedCountry(EFDbContext context)
        {
            var count = context.Countries.Count();
            if (count <= 0)
            {
                var countries = new List<Country>
                {
                    new Country
                    {
                        Name="Sweden"
                    },
                new Country
                    {
                        Name="German"
                    }
                };
                context.AddRange(countries);
                context.SaveChanges();
            }
        }
        public static void SeedDeveloperCompany(EFDbContext context)
        {
            var count = context.Developers.Count();
            if (count <= 0)
            {
                var companies = new List<DeveloperCompany>
                {
                    new DeveloperCompany
                    {
                        Country=context.Countries.FirstOrDefault(x=>x.Name=="Sweden"),
                        Name="Paradox Interactive",
                        User=context.Users.FirstOrDefault(x=>x.Name=="ParadoxDev")                        
                    }
                };
                context.AddRange(companies);
                context.SaveChanges();
            }
        }
        public static void SeedGenres(EFDbContext context)
        {
            var count = context.Genres.Count();
            if (count <= 0)
            {
                var genres = new List<Genre>
                {
                    new Genre
                    {
                        Name="Strategy"
                    }
                };
                context.Genres.AddRange(genres);
                context.SaveChanges();
            }
        }
        public static void SeedGames(EFDbContext context)
        {
            var count = context.Games.Count();
            if (count <= 0)
            {
                var games = new List<Game>
                {
                    new Game
                    {
                        Name="Europa Universalis 4",
                        Price=40,
                        Genres=context.Genres.FirstOrDefault(x=>x.Name=="Strategy")
                    }
                };
                context.AddRange(games);
                context.SaveChanges();
            }
        }
        public static void SeedData(IServiceProvider services, IHostingEnvironment env, IConfiguration config)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                var managerUser = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                var context = scope.ServiceProvider.GetRequiredService<EFDbContext>();
                SeederDB.SeedRoles(managerRole);
                SeederDB.SeedUsers(managerUser, context);
                SeederDB.SeedCountry(context);
                SeederDB.SeedDeveloperCompany(context);
                SeederDB.SeedGenres(context);
                SeederDB.SeedGames(context);
            }
        }
    }
}
