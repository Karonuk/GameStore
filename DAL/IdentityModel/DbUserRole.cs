using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.IdentityModel
{
    public class DbUserRole:IdentityUserRole<string>
    {
        public virtual DbUser User { get; set; }
        public virtual DbRole Role { get; set; }
    }
}
