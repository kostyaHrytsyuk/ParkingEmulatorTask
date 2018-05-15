using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingRestApi.Services;
using ParkingEmulatorLogic;
using Newtonsoft.Json;

namespace ParkingRestApi.Controllers
{
    [Produces("application/json")]
    [Route("/Parking/[action]/")]
    public class ParkingController : Controller
    {
        private ParkingService service { get; set; }

        public ParkingController(ParkingService service)
        {
            this.service = service;

            if (service.parking.Cars.Count == 0)
            {
                service.parking.Cars.Add(new Car(CarType.Passenger, 20));
                service.parking.Cars.Add(new Car(CarType.Bus, 100));
                service.parking.Cars.Add(new Car(CarType.Truck, 200));
            }
        }

        #region Non Parameterized Get Requests

        // GET: Parking/GetAllCars
        [HttpGet]
        public IActionResult GetAllCars()
        {
            return Json(service.GetAllCars());
        }

        // GET: Parking/GetFreeSpace
        [HttpGet]
        public IActionResult GetFreeSpace()
        {
            return Json(service.GetFreeSpace());
        }

        // GET: Parking/GetBookedSpace
        [HttpGet]
        public IActionResult GetBookedSpace()
        {
            return Json(service.GetBookedSpace());
        }

        // GET: Parking/GetCurrentProfit
        [HttpGet]
        public IActionResult GetCurrentProfit()
        {
            return Json(service.GetCurrentProfit());
        }

        // GET: Parking/GetTransactionLog
        [HttpGet]
        public IActionResult GetTransactionLog()
        {
            return Json(service.GetTransactionLog());
        }

        // GET: Parking/GetTransactionsForTheLastMinute
        [HttpGet]
        public IActionResult GetTransactionsForTheLastMinute()
        {
            return Json(service.GetTransactionsForTheLastMinute());
        }

        #endregion

        #region Parameterized Get Requests

        // GET: /Parking/GetCarById/id?
        [HttpGet("{id}")]
        public IActionResult GetCarById(int id)
        {
            return Json(service.GetAllCars().Where(car => car.Id == id));
        }

        // GET: /Parking/GetTransactionsForTheLastMinuteByCarId/id?
        [HttpGet("{id}")]
        public IActionResult GetTransactionsForTheLastMinuteByCarId(int id)
        {
            return Json(service.GetTransactionsForTheLastMinuteByCarId(id));
        }

        #endregion
        


        // POST: api/Parking
        [HttpPost]
        [Route("/Parking/AddCar/{carTypeKey}/{firstPayment}")]
        public IActionResult AddCar(int carTypeKey, double firstPayment)
        {
            if (carTypeKey < 1 || carTypeKey > 4)
            {
                return NotFound();
            }
            else
            {
                service.AddCar(carTypeKey, firstPayment);
                return NoContent();
            }
        }
        
        // PUT: api/Parking/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = service.RemoveCarFromParking(id);

            if (!isDeleted)
            {
                return NotFound();
            }
            else
            {
                return new NoContentResult();
            }
        }
    }
}
