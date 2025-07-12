using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCozyShop.Models;

namespace WebCozyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Hehe()
        {
            return View();
        }

        public IActionResult Huhu()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
