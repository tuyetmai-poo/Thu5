namespace Thu5.Models
{
    public static class Data
    {
        public static List<Driver> Drivers = new List<Driver>()
        {
            new Driver { Id = 1, Name = "Tài xế A", IsOnline = true },
            new Driver { Id = 2, Name = "Tài xế B", IsOnline = true }
        };

        public static List<Ride> Rides = new List<Ride>();
    }
}
