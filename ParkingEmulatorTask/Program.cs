using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingEmulatorTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Parking parking = Parking.Instance;
            //Settings.ParkingCustomization();
                        
            parking.AddCar();

            Console.ReadKey();
        }
    }
}
