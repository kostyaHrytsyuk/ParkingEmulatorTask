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
        public readonly Parking Parking;

        public ParkingService()
        {
            Parking = Parking.Instance;
        }

        public List<Car> GetAllCars()
        {
            return Parking.Cars;             
        }

        
    }
}
