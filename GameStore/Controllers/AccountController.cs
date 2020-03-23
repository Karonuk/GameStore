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
        public readonly UserManager<DbUser> _userManager;
        public AccountController(SignInManager<DbUser> signInManager, EFDbContext context, UserManager<DbUser> userManager)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfile = new UserProfile
                {
                    Name = model.Name,
                    RegistrationDate = DateTime.Now
                };

                DbUser user = new DbUser
                {
                    Email = model.Email,
                    UserName = model.Name,
                    UserProfile = userProfile
                };

                var rolename = "User";
                var result = await _userManager.CreateAsync(user, model.Password);
                result = _userManager.AddToRoleAsync(user, rolename).Result;
                
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel { MsgEmail = "", MsgPassword = "" });
        }


        public async Task<IActionResult> Login(LoginViewModel user)
        {
            var User = await _context.Users.Include(x => x.User).FirstOrDefaultAsync(x => x.User.Email == user.Email);
            if (User != null)
            {
                var result = _signInManager.PasswordSignInAsync(User.User, user.Password, false, false).Result;
                if (result.Succeeded)
                {
                    await Authenticate(User.User.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(new LoginViewModel
                    {
                        Email = user.Email,
                        Password = user.Password,
                        MsgPassword = "Invalid password"
                    });
                }
            }
            else
            {
                return View(new LoginViewModel
                {
                    Email = user.Email,
                    Password = user.Password,
                    MsgEmail = "Invalid email"
                });
            }
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