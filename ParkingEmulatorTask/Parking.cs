using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.IO;

namespace ParkingEmulatorTask
{
    class Parking
    {
        #region Parking Creation

        private static readonly Lazy<Parking> lazyInstance = new Lazy<Parking>(() => new Parking());

        public static Parking Instance { get { return lazyInstance.Value; } }

        private static Timer chargingTimer;
        private static Timer transactionLoggingTimer;


        private Parking()
        {
            Console.WriteLine("Hello!\nLet's create a parking");
            Thread.Sleep(1200);
            Menu.ParkingCustomization();

            var auto = new AutoResetEvent(false);
            TimerCallback chargingCallback = new TimerCallback(ChargeFee);
            TimerCallback transactionLoggingCallback = new TimerCallback(LogTransactions);
            chargingTimer = new Timer(chargingCallback, auto, Settings.Timeout, Settings.Timeout);
            transactionLoggingTimer = new Timer(transactionLoggingCallback, auto, Settings.LoggingInterval, Settings.LoggingInterval);
        }
                
        #endregion

        #region Properties
        private static List<Car> cars = new List<Car>();
        private static List<int> carIds = new List<int>();
        private List<Transaction> transactions = new List<Transaction>();
        private double lastMinuteProfit;

        public List<Transaction> LastMinuteTransactions { get; set; } = new List<Transaction>();
        public List<Car> Cars { get { return cars; } }
        public static List<int> CarsIds { get { return carIds; } }
        public double PassiveBalance { get; set; }
        public double ActiveBalance  { get; set; }
        #endregion

        #region Data Getting Methods 
        public void GetAllCars()
        {
            if (cars.Count == 0)
            {
                Console.WriteLine("There is no cars on the parking\n");
            }
            else
            {
                Console.WriteLine("CarId\tCar Type\tBalance");

                foreach (var car in cars)
                {
                    Console.WriteLine(car.Id + "\t" + car.CarType + "\t\t\t" + car.Balance.ToString("F"));
                }
            }
        }
        
        public static void GetLastMinuteProfit()
        {

        }
        #endregion

        public void AddCar()
        {
            CarType carType = Menu.InputedCarValidation();
                        
            var firstPayment = Menu.InputedBalanceValidation();            

            var car = new Car(firstPayment, carType);

            cars.Add(car);
            carIds.Add(car.Id);
            Console.WriteLine($"Vehicle {car.CarType} with Id {car.Id} was added to parking");
            Thread.Sleep(1500);           
        }

        public void DeleteCar(int carId)
        {
            var carDel = cars.Find(item => item.Id == carId);

            if (carDel == null)
            {
                Console.WriteLine($"There is no car with such {carId} on the parking");
            }
            else
            {
                if (carDel.Balance < 0)
                {
                    Menu.CarBalanceRefilling(carDel, this);
                    DeleteCar(carId);
                }
                else
                {
                    cars.Remove(carDel);
                    Console.WriteLine("Now you can take your car from the parking\nHave a nice day!");
                    Thread.Sleep(2000);
                }
            }
                        
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
                lastMinuteProfit += feeSize;
                var transaction = new Transaction(car.Id, feeSize);
                transactions.Add(transaction);
                LastMinuteTransactions.Add(transaction);
            }
        }    
        
        private void LogTransactions(object stateInfo)
        {
            Transaction.AddToTransactionLog(LastMinuteTransactions);
            
            lastMinuteProfit = 0;
            
            LastMinuteTransactions.Clear();
        }

        public void CloseParking()
        {
            chargingTimer.Dispose();
            transactionLoggingTimer.Dispose();
            Transaction.AddToTransactionLog(LastMinuteTransactions);
        }
    }
}