﻿using System;

namespace ParkingEmulatorTask
{
    class Car
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public CarType CarType { get; set; }

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
