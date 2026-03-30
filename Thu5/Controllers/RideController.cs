using Microsoft.AspNetCore.Mvc;
using Thu5.Data;
using Thu5.Models;
using System.Linq;

namespace Thu5.Controllers
{
    public class RideController : Controller
    {
        private readonly AppDbContext _context;

        public RideController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("Username");

            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }
    }
}