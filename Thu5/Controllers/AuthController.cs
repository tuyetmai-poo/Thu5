using Microsoft.AspNetCore.Mvc;
using Thu5.Models;

namespace Thu5.Controllers
{
    public class AuthController : Controller
    {
        // ====== ĐĂNG KÝ ======
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                Data.Users.Add(user);

                // đăng ký xong → chuyển qua Login
                return RedirectToAction("Login");
            }
            return View();
        }

        // ====== ĐĂNG NHẬP ======
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var check = Data.Users
                .FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (check != null)
            {
                // đăng nhập thành công → về Index (màn hiện tại)
                return RedirectToAction("Index", "Ride");
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
            return View();
        }
    }
}
