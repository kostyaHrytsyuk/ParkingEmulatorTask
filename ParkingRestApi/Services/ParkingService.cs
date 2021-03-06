﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using ParkingEmulatorLogic;
using ParkingRestApi.Models;
using Newtonsoft.Json;

namespace ParkingRestApi.Services
{
    public class ParkingService
    {
        public readonly Parking parking;

        public ParkingService()
        {
            parking = Parking.Instance;
        }

        #region Get Methods Without Parameters

        public List<Car> GetAllCars()
        {
            return parking.Cars;
        }

        public int GetFreeSpace()
        {
            return parking.FreeSpace;
        }

        public int GetBookedSpace()
        {
            return parking.Cars.Count;
        }

        public Dictionary<string, double> GetCurrentProfit()
        {
            return new Dictionary<string, double>()
            {
                { "Common Balance" , parking.CommonBalance  },
                { "Active Balance" , parking.ActiveBalance  },
                { "Passive Balance", parking.PassiveBalance }
            };
        }

        public string GetTransactionLog()
        {
            return Transaction.GetTransactionLog();
        }

        public List<Transaction> GetTransactionsForTheLastMinute()
        {
            return parking.LastMinuteTransactions;
        }
        #endregion


        #region Get Methods With Parameters
        public IEnumerable<Transaction> GetTransactionsForTheLastMinuteByCarId(int carId)
        {
            return parking.LastMinuteTransactions.Where(car => car.CarId == carId);
        }
        
        public bool RemoveCarFromParking(int id)
        {
            return parking.RemoveCarFromParking(id);
        }
        #endregion

        #region Post Methods

        public void AddCar(int carTypeKey, double firstPayment)
        {
            var newCar = new Car( (CarType) carTypeKey, firstPayment);
            parking.Cars.Add(newCar);
        }

        #endregion

        public void CarBalanceRefilling(double fine, int carId)
        {
            parking.CarBalanceRefilling(fine, carId);
        }

    }
}
