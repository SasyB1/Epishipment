using Epishipment.Services;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
