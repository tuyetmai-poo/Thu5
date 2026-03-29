using Microsoft.AspNetCore.Mvc;
using Thu5.Models;

namespace Thu5.Controllers
{
    public class RideController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Book(string pickup, string destination,
            double lat, double lng,
            double destLat, double destLng)
        {
            var driver = FindDriver();

            var ride = new Ride
            {
                Id = Data.Rides.Count + 1,
                Pickup = pickup,
                Destination = destination,
                CustomerLat = lat,
                CustomerLng = lng,
                DestinationLat = destLat,
                DestinationLng = destLng,
                Status = "Searching",
                DriverId = driver?.Id
            };

            Data.Rides.Add(ride);

            return RedirectToAction("Waiting", new { id = ride.Id });
        }

        public IActionResult Waiting(int id)
        {
            var ride = Data.Rides.FirstOrDefault(r => r.Id == id);
            return View(ride);
        }

        public IActionResult DriverPanel()
        {
            return View(Data.Rides);
        }

        public IActionResult Accept(int id)
        {
            var ride = Data.Rides.FirstOrDefault(r => r.Id == id);
            if (ride != null) ride.Status = "Accepted";
            return RedirectToAction("DriverPanel");
        }

        public IActionResult Arrived(int id)
        {
            var ride = Data.Rides.FirstOrDefault(r => r.Id == id);
            if (ride != null) ride.Status = "Arrived";
            return RedirectToAction("DriverPanel");
        }

        public IActionResult Complete(int id)
        {
            var ride = Data.Rides.FirstOrDefault(r => r.Id == id);
            if (ride != null) ride.Status = "Completed";
            return RedirectToAction("DriverPanel");
        }

        private Driver FindDriver()
        {
            var available = Data.Drivers.Where(d => d.IsOnline).ToList();
            if (available.Count == 0) return null;

            Random rd = new Random();
            return available[rd.Next(available.Count)];
        }
    }
}
