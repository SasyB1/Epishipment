using Epishipment.Models.Dto;
using Epishipment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epishipment.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;

        public AuthController(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username, Password")] LoginDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid login attempt";
                return View();
            }

            var user = _userService.GetUser(loginDto);
            if(user == null) {
                TempData["Error"] = "Invalid login attempt";
                return View();
            }
            _authService.Login(user);
            TempData["Success"] = "Login successful";
            return RedirectToAction("Index", "Home");

        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            _authService.Logout();
            TempData["Success"] = "Logout successful";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       async  Task<IActionResult> Register ([Bind("Username, Password")] RegisterDto registerDto)
        {
          if(!ModelState.IsValid)
                {
                TempData["Error"] = "Invalid registration attempt";
                return View();
            }
          var user = _userService.AddUser(registerDto);
            if(user != null) {
                    TempData["Error"] = "User already exists";
                    return View();
                }
              
                TempData["Success"] = "Registration successful";
                return RedirectToAction("Index" ,"Home");
        }
    }
}
