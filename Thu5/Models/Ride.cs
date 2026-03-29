namespace Thu5.Models
{
    public class Ride
    {
        public int Id { get; set; }
        public string Pickup { get; set; }
        public string Destination { get; set; }

        public double? CustomerLat { get; set; }
        public double? CustomerLng { get; set; }

        public double? DestinationLat { get; set; }
        public double? DestinationLng { get; set; }

        public string Status { get; set; } // Searching, Accepted, Arrived, Completed
        public int? DriverId { get; set; }
    }
}
