using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.IO;

namespace ParkingEmulatorLogic
{
    public class Parking
    {
        #region Parking Creation

        private static readonly Lazy<Parking> lazyInstance = new Lazy<Parking>(() => new Parking());

        public static Parking Instance { get { return lazyInstance.Value; } }

        private static Timer chargingTimer;
        private static Timer transactionLoggingTimer;


        private Parking()
        {
            var auto = new AutoResetEvent(false);
            TimerCallback chargingCallback = new TimerCallback(ChargeFee);
            TimerCallback transactionLoggingCallback = new TimerCallback(LogTransactions);
            chargingTimer = new Timer(chargingCallback, auto, Settings.Timeout, Settings.Timeout);
            transactionLoggingTimer = new Timer(transactionLoggingCallback, auto, Settings.LoggingInterval, Settings.LoggingInterval);
        }
                
        #endregion

        #region Properties
        private static List<int> carIds = new List<int>();
        private List<Transaction> transactions = new List<Transaction>();
        private double lastMinuteProfit;

        public List<Transaction> LastMinuteTransactions { get; set; } = new List<Transaction>();
        public List<Car> Cars { get; set; } = new List<Car>();
        public static List<int> CarsIds { get { return carIds; } }
        public double PassiveBalance { get; set; }
        public double ActiveBalance  { get; set; }
        #endregion

        #region Data Getting Methods 
        public void GetAllCars()
        {
            if (Cars.Count == 0)
            {
                Console.WriteLine("There is no cars on the parking\n");
            }
            else
            {
                Console.WriteLine("CarId\tCar Type\tBalance");

                foreach (var car in Cars)
                {
                    Console.WriteLine(car.Id + "\t" + car.CarType + "\t\t\t" + car.Balance.ToString("F"));
                }
            }
        }
        
        public static void GetLastMinuteProfit()
        {

        }
        #endregion
        
        private void ChargeFee(object stateInfo)
        {
            foreach (var car in Cars)
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