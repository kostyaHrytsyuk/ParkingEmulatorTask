using System;

namespace ParkingEmulatorTask
{
    class Car
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public CarType CarType { get; set; }

        public Car(decimal firstPayment , CarType carType)
        {
            Id = new Random().Next(100, 999);
            Balance = firstPayment;
            CarType = carType;
        }
    }
}
