using BlogApp.DataAccess.Abstract;
using BlogApp.Entities;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;    
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user= await _userRepository.Users.FirstOrDefaultAsync(u=>u.Email==model.Email || u.Nickname==model.Nickname);
                if (user == null)
                {
                    _userRepository.CreateUser(new User
                    {
                        UserName = model.UserName,
                        UserSurname = model.UserSurname,
                        Nickname = model.Nickname,
                        Email = model.Email,
                        Password = model.Password,
                        UserImage="User-1.png"
                    });
                    return RedirectToAction("login");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya E-posta başka bir kullanıcıya ait!");
                }
                
            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isUser = await _userRepository.Users.FirstOrDefaultAsync(u=>u.Email== model.Email && u.Password== model.Password);
                if (isUser != null)
                {
                    var userClaims = new List<Claim>
                    {
                        new(ClaimTypes.NameIdentifier, isUser.UserId.ToString()),
                        new(ClaimTypes.Name, isUser.Nickname ?? ""),
                        new(ClaimTypes.Surname, isUser.UserSurname ?? ""),
                        new(ClaimTypes.UserData, isUser.UserImage ?? "")
                    };

                    if (isUser.Email == "cemo@mail.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        //IsPersistent = true
                        
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya Parola hatalı!");
                }
            }
            
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");

        }

        public async Task<IActionResult> Profile(string Nickname)
        {
            var user= await _userRepository.Users.Include(x => x.Posts).Include(x => x.Comments).ThenInclude(x=>x.Post).FirstOrDefaultAsync(u=>u.Nickname==Nickname);
            return View(user);
        }

    }
}
