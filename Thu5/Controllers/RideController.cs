using Microsoft.AspNetCore.Mvc;
using Thu5.Models;
using Thu5.Data;
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

        // ====== MÀN CHÍNH ======
        public IActionResult Index()
        {
            return View();
        }

        // ====== ĐẶT XE ======
        [HttpPost]
        public IActionResult Book(string pickup, string destination,
            double lat, double lng,
            double destLat, double destLng)
        {
            var driver = FindDriver();

            var ride = new Ride
            {
                Pickup = pickup,
                Destination = destination,
                CustomerLat = lat,
                CustomerLng = lng,
                DestinationLat = destLat,
                DestinationLng = destLng,
                Status = "Searching",
                DriverId = driver?.Id
            };

            _context.Rides.Add(ride);
            _context.SaveChanges();

            return RedirectToAction("Waiting", new { id = ride.Id });
        }

        // ====== MÀN CHỜ ======
        public IActionResult Waiting(int id)
        {
            var ride = _context.Rides.FirstOrDefault(r => r.Id == id);
            return View(ride);
        }

        // ====== MÀN TÀI XẾ ======
        public IActionResult DriverPanel()
        {
            var rides = _context.Rides.ToList();
            return View(rides);
        }

        // ====== NHẬN CHUYẾN ======
        public IActionResult Accept(int id)
        {
            var ride = _context.Rides.FirstOrDefault(r => r.Id == id);
            if (ride != null)
            {
                ride.Status = "Accepted";
                _context.SaveChanges();
            }
            return RedirectToAction("DriverPanel");
        }

        // ====== ĐẾN ĐIỂM ĐÓN ======
        public IActionResult Arrived(int id)
        {
            var ride = _context.Rides.FirstOrDefault(r => r.Id == id);
            if (ride != null)
            {
                ride.Status = "Arrived";
                _context.SaveChanges();
            }
            return RedirectToAction("DriverPanel");
        }

        // ====== HOÀN THÀNH ======
        public IActionResult Complete(int id)
        {
            var ride = _context.Rides.FirstOrDefault(r => r.Id == id);
            if (ride != null)
            {
                ride.Status = "Completed";
                _context.SaveChanges();
            }
            return RedirectToAction("DriverPanel");
        }

        // ====== TÌM TÀI XẾ ======
        private Driver FindDriver()
        {
            var available = _context.Drivers
                                    .Where(d => d.IsOnline)
                                    .ToList();

            if (available.Count == 0) return null;

            Random rd = new Random();
            return available[rd.Next(available.Count)];
        }
    }
}