using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

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

        #region Properties
        private List<Car> cars = new List<Car>();

        public List<Car> Cars { get { return cars; } }

        public List<Transaction> Transactions { get; set; }

        public decimal Balance { get; set; }
        #endregion

        public void GetFreeParkingSpace()
        {
            var freeSpaces = Settings.ParkingSpace - Cars.Count;
            Console.WriteLine("Current parking fullness:");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Free spaces: {freeSpaces}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Booked places: {Cars.Count}");

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        #region Car Addition
        public void AddCar()
        {
            decimal firstPayment = InputedBalanceValidation();

            CarType carType = InputedCarValidation();

            var car = new Car(firstPayment, carType);

            cars.Add(car);

            Console.WriteLine($"Vehicle {car.CarType} with Id {car.Id} was added to parking");            
        }

        private decimal InputedBalanceValidation()
        {
            Console.WriteLine("Input your first payment");
            decimal firstPayment;
            var inputValue = Console.ReadLine();

            if (decimal.TryParse(inputValue, out firstPayment) && firstPayment > 0)
            {
                return firstPayment;
            }
            else
            {
                Console.WriteLine("You entered a wrong value!");
                Console.WriteLine(new string('-',15));
                return firstPayment = InputedBalanceValidation();
            }
        }

        private CarType InputedCarValidation()
        {
            Console.WriteLine("Car Types:");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("1 - Passenger");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("2 - Truck");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("3 - Bus");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("4 - Motorcycle");

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("Enter your car type:");

            var inputValue = Console.ReadLine()[0].ToString();
            uint carTypeKey = 0;
            CarType carType = CarType.Passenger;
            if (uint.TryParse(inputValue, out carTypeKey) && (carTypeKey < 5 && carTypeKey > 0))
            {
                switch (carTypeKey)
                {
                    case 1:
                        return CarType.Passenger;
                    case 2:
                        return CarType.Truck;
                    case 3:
                        return CarType.Bus;
                    case 4:
                        return CarType.Motorcycle;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You entered a wrong value!");
                Console.WriteLine(new string('-', 15));
                return carType = InputedCarValidation();
            }

            return carType;
        }
        #endregion
    }
}
