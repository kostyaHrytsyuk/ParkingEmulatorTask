using System;

namespace ParkingEmulatorTask
{
    class Car
    {
        public readonly int Id;
        public double Balance { get; set; }
        public readonly CarType CarType;

        public Car(double firstPayment , CarType carType)
        {
            Id = GenerateId();
            Balance = firstPayment;
            CarType = carType;
        }

        private int GenerateId()
        {
            var randId = 0;
            do
            {
                randId = new Random().Next(100, 999);
            } while (Parking.CarsIds.Contains(randId));

            return randId;
        }
    }
}
