using System;

namespace ParkingEmulatorTask
{
    class Transaction
    {
        public DateTime TransactionTime { get; set; }

        public int CarId { get; set; }

        public double WrittenOffMoney { get; set; }

        public Transaction(int carId, double fee)
        {
            CarId = carId;
            WrittenOffMoney = fee;
            TransactionTime = DateTime.Now;
        }

    }
}
