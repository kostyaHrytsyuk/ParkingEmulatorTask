using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingEmulatorLogic;

namespace ParkingRestApi.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public CarType CarType { get; set; }
    }
}
