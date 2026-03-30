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

        // ===== REGISTER =====
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var exists = _context.Users.Any(u => u.Username == user.Username);

            if (exists)
            {
                ViewBag.Error = "Tài khoản đã tồn tại";
                return View(user);
            }

            user.Role = "Customer";

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login", "Auth"); // 👈 QUAN TRỌNG
        }

        // ===== LOGIN =====
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var check = _context.Users
                .FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (check != null)
            {
                HttpContext.Session.SetString("Username", check.Username);
                HttpContext.Session.SetString("UserId", check.Id.ToString());

                return RedirectToAction("Index", "Ride"); // 👈 QUAN TRỌNG
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
            return View();
        }
    }
}