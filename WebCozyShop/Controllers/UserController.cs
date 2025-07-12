using Microsoft.AspNetCore.Mvc;
using WebCozyShop.Models;
using WebCozyShop.Requests;
using WebCozyShop.Services.Interface;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult UserProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            User user = _userService.GetUserById((int)userId!)!;
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return View();
            }

            UpdateUserRequest updateRequest = new UpdateUserRequest
            {
                Id = user.UserID,
                FullName = user.FullName,
                Phone = user.Phone,
                Dob = user.Dob,
            };

            return View(new ProfileViewModel { User = user, UpdateRequest = updateRequest });
        }

        [HttpPost]
        public IActionResult UpdateProfile(ProfileViewModel request)
        {
            UpdateUserRequest updateUserRequest = request.UpdateRequest;
            string msg = _userService.UpdateUser(updateUserRequest);

            TempData["Success"] = msg;
            return RedirectToAction("UserProfile");
        }

        [HttpGet]
        public IActionResult ChangePassword(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordRequest request)
        {
            int id = HttpContext.Session.GetInt32("UserId") ?? 0;

            bool isChanged = _userService.ChangePassword(id, request);
            if (!isChanged)
            {
                TempData["Error"] = "Password not correct. Please try again.";
                return RedirectToAction("ChangePassword");
            }

            TempData["Success"] = "Password changed successfully.";
            return RedirectToAction("ChangePassword");
        }
    }
}
