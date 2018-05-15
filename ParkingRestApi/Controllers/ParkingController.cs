using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingRestApi.Services;
using ParkingEmulatorLogic;

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
                service.parking.Cars.Add(new Car(20, CarType.Passenger));
                service.parking.Cars.Add(new Car(100, CarType.Bus));
            }
        }

        #region Non Parameterized Get Requests

        // GET: Parking/GetAllCars
        [HttpGet]
        public List<Car> GetAllCars()
        {
            return service.GetAllCars();
        }

        // GET: Parking/GetFreeSpace
        [HttpGet]
        public int GetFreeSpace()
        {
            return service.GetFreeSpace();
        }

        // GET: Parking/GetBookedSpace
        [HttpGet]
        public int GetBookedSpace()
        {
            return service.GetBookedSpace();
        }

        #endregion

        // GET: api/Parking/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<Car> GetById(int id)
        {
            return service.GetAllCars().Where(car => car.Id == id);
        }

        // POST: api/Parking
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
