using System;
using System.Collections.Generic;
using System.Threading;

namespace ParkingEmulatorTask
{
    class Parking
    {
        #region Parking Creation

        private static readonly Lazy<Parking> lazyInstance = new Lazy<Parking>(() => new Parking());

        public static Parking Instance { get { return lazyInstance.Value; } }

        private Parking()
        {
            Console.WriteLine("Hello!\nLet's create a parking");
            Thread.Sleep(1200);
            Settings.ParkingCustomization();
        }

        #endregion
        
        public List<Car> Cars { get; }

        public List<Transaction> Transactions { get; set; }

        public decimal Balance { get; set; }
    }
}
