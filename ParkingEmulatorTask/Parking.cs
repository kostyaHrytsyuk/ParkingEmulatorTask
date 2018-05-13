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

        Timer timer;

        private Parking()
        {
            Console.WriteLine("Hello!\nLet's create a parking");
            Thread.Sleep(1200);
            Menu.ParkingCustomization();

            var auto = new AutoResetEvent(false);
            TimerCallback callback = new TimerCallback(ChargeFee);
            timer = new Timer(ChargeFee, auto, Settings.Timeout, Settings.Timeout);
        }
                
        #endregion

        #region Properties
        private List<Car> cars = new List<Car>();
        private static List<int> carIds = new List<int>();
        private static double passiveBalance { get; set; }
        private static double activeBalance { get; set; }
        
        public static double PassiveBalance { get { return passiveBalance; } }
        public static double ActiveBalance  { get { return activeBalance;  } }

        public List<Car> Cars { get { return cars; } }

        public static List<int> CarsIds { get { return carIds; } }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        
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

        //Car Addition
        public void AddCar()
        {
            var firstPayment = Menu.InputedBalanceValidation();

            CarType carType = Menu.InputedCarValidation();

            var car = new Car(firstPayment, carType);

            cars.Add(car);
            carIds.Add(car.Id);
            Console.WriteLine($"Vehicle {car.CarType} with Id {car.Id} was added to parking");            
        }        
        
        //Car Deletion
        public void DeleteCar(int carId)
        {
            var carDel = Cars.Where(item => item.Id == carId);
            Cars.Remove(carDel.First());
        }               

        //Charging fees
        private void ChargeFee(object stateInfo)
        {
            foreach (var car in cars)
            {
                double feeSize = Settings.PriceSet[car.CarType];
                if (car.Balance < Settings.PriceSet[car.CarType])
                {
                    feeSize += feeSize*Settings.Fine;
                    car.Balance -= feeSize;
                    activeBalance += feeSize;
                }
                else
                {
                    car.Balance -= feeSize;
                    passiveBalance += feeSize;
                }                

                var transaction = new Transaction(car.Id, feeSize);
                Transactions.Add(transaction);

                Console.WriteLine("Fees charged!");
            }
        }                
    }
}
