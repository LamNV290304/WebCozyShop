using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebCozyShop.Helper;
using WebCozyShop.Models;
using WebCozyShop.Requests;
using WebCozyShop.Services.Interface;

namespace WebCozyShop.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IAuthenService _authenService;

        public AuthenController(IAuthenService authenService)
        {
            _authenService = authenService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserRequest loginUser)
        {
            User user = _authenService.DoLogin(loginUser);

            if (user.Username.IsNullOrEmpty())
            {
                TempData["Error"] = "Sai tài khoản hoặc mật khẩu";
                return View(loginUser);
            }

            HttpContext.Session.SetInt32("UserId", user.UserID);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Authen");
        }
    }
}
