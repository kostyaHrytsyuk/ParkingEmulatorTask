using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingEmulatorTask
{
    static class Settings
    {
        public static TimeSpan Timeout { get; set; } = new TimeSpan(0,0,3);

        public static Dictionary<CarType, decimal> PriceSet => new Dictionary<CarType, decimal>()
        {
             { CarType.Passenger, 3.00m }
           , { CarType.Truck, 10.00m }
           , { CarType.Bus, 5.00m}
           , { CarType.Motorcycle,1.50m}
        };

        public static int ParkingSpace { get; set; }

        public static double Fine { get; set; } = 0.3;
    }
}
