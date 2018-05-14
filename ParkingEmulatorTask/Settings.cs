using System;
using System.Collections.Generic;

namespace ParkingEmulatorTask
{
    static class Settings
    {
        #region Properties
        public static readonly TimeSpan LoggingInterval = new TimeSpan(0, 1, 0);

        public static TimeSpan Timeout { get; set; } = new TimeSpan(0, 0, 3);
        public static int ParkingSpace { get; set; } = 50;
        public static double Fine      { get; set; } = 0.03;
        #endregion

        public static Dictionary<CarType, double> PriceSet => new Dictionary<CarType, double>()
        {
             { CarType.Passenger, 3.00 }
           , { CarType.Truck, 10.00 }
           , { CarType.Bus, 5.00 }
           , { CarType.Motorcycle, 1.50 }
        };                        
    }
}