using System;
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

        #endregion


        public bool RemoveCarFromParking(int id)
        {
            return parking.RemoveCarFromParking(id);
        }

    }
}
