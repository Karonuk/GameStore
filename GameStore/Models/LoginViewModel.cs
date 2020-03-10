using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class LoginViewModel
    {
        [Required,Compare("Email")]        
        public string Email { get; set; }
        public string Password { get; set; }        
        public string MsgEmail { get; set; }
        public string MsgPassword { get; set; }

    }
}
