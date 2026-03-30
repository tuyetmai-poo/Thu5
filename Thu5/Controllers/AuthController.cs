using Microsoft.AspNetCore.Mvc;
using Thu5.Models;
using Thu5.Data;
using System.Linq;

namespace Thu5.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

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
                // kiểm tra trùng username
                var exists = _context.Users
                    .Any(u => u.Username == user.Username);

                if (exists)
                {
                    ViewBag.Error = "Tài khoản đã tồn tại";
                    return View();
                }

                _context.Users.Add(user);
                _context.SaveChanges();

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
            var check = _context.Users
                .FirstOrDefault(u => u.Username == user.Username
                                  && u.Password == user.Password);

            if (check != null)
            {
                // lưu session
                HttpContext.Session.SetString("Username", check.Username);

                return RedirectToAction("Index", "Ride");
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
            return View();
        }

        // ====== ĐĂNG XUẤT ======
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}