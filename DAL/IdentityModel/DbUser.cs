using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.IdentityModel
{
    public class DbUser:IdentityUser
    {
        public ICollection<DbUserRole> UserRoles { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
