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
    [Route("api/Parking")]
    public class ParkingController : Controller
    {
        private ParkingService service { get; set; }

        public ParkingController(ParkingService service)
        {
            this.service = service;

            if (service.Parking.Cars.Count == 0)
            {
                service.Parking.Cars.Add(new Car(20, CarType.Passenger));
                service.Parking.Cars.Add(new Car(100, CarType.Bus));
            }
        }

        // GET: api/Parking
        [HttpGet]
        public List<Car> GetAllCars()
        {
            return service.GetAllCars();
        }

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
        public void Delete(int id)
        {
        }
    }
}
