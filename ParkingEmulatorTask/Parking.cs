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
        private List<Transaction> transactions = new List<Transaction>();

        public List<Car> Cars { get { return cars; } }
        public static List<int> CarsIds { get { return carIds; } }
        public static double PassiveBalance { get; set; }
        public static double ActiveBalance  { get; set; } 
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

        public void GetAllCars()
        {
            Console.WriteLine("CarId\tWritten Off Money\tTransaction Time");

            foreach (var car in cars)
            {
                Console.WriteLine(car.Id + "\t" + car.CarType + "\t\t\t" + car.Balance.ToString("F"));

                Console.WriteLine($"{car.Id}\t");
            }
        }
        
        public void AddCar()
        {
            var firstPayment = Menu.InputedBalanceValidation();

            CarType carType = Menu.InputedCarValidation();

            var car = new Car(firstPayment, carType);

            cars.Add(car);
            carIds.Add(car.Id);
            Console.WriteLine($"Vehicle {car.CarType} with Id {car.Id} was added to parking");
            Thread.Sleep(1500);
            Console.Clear();            
        }        
        
        public void DeleteCar(int carId)
        {
            var carDel = Cars.Find(item => item.Id == carId);

            if (carDel == null)
            {
                Console.WriteLine($"There is no car with such {carId} on the parking");
            }
            else
            {
                if (carDel.Balance < 0)
                {
                    Menu.CarBalanceRefilling(carDel);
                    DeleteCar(carId);
                }
                else
                {
                    Cars.Remove(carDel);
                    Console.WriteLine("Now you can take your car from the parking\nHave a nice day!");
                    Thread.Sleep(2000);
                }
            }

            Console.Clear();
        }               
                
        private void ChargeFee(object stateInfo)
        {
            foreach (var car in cars)
            {
                double feeSize = Settings.PriceSet[car.CarType];
                if (car.Balance < Settings.PriceSet[car.CarType])
                {
                    feeSize += feeSize*Settings.Fine;
                    car.Balance -= feeSize;
                    ActiveBalance += feeSize;
                }
                else
                {
                    car.Balance -= feeSize;
                    PassiveBalance += feeSize;
                }                

                var transaction = new Transaction(car.Id, feeSize);
                transactions.Add(transaction);                
            }
        }                
    }
}
