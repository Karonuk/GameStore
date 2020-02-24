using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using DAL.IdentityModel;
using GameStore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<DbUser> _signInManager;
        private EFDbContext _context;
        public AccountController(SignInManager<DbUser> signInManager,EFDbContext context)
        {
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public Task Login(LoginViewModel user)
        {
            var User = _context.Users.FirstOrDefault(x=>x.User.Email==user.Email);
            if (User != null)
            {
                var result = _signInManager.PasswordSignInAsync(User.User, user.Password, false, false).Result;
                if (result.Succeeded)
                {
                    Authenticate(User.User.Email);                    
                }
            }
            return RedirectToAction("Login","Home");
        }

        private async Task Authenticate(string userName)
        {
            
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}