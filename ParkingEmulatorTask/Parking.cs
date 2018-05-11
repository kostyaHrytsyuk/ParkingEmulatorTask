using System.Collections.Generic;

namespace ParkingEmulatorTask
{
    class Parking
    {
        public List<Car> Cars { get; }

        public List<Transaction> Transactions { get; set; }

        public decimal Balance { get; set; }
    }
}
