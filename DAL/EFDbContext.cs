using DAL.Entities;
using DAL.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class EFDbContext : IdentityDbContext<DbUser, DbRole, string, IdentityUserClaim<string>,
    DbUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<DeveloperCompany> Developers { get; set; }
        public DbSet<UserProfile> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameLibrary> GameLibraries { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
