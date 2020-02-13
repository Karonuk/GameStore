using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.IdentityModel
{
    public class DbRole:IdentityRole<string>
    {
        public ICollection<DbUserRole> UserRoles { get; set; }
    }
}
