using System;

namespace ParkingEmulatorTask
{
    class Transaction
    {
        public DateTime TransactionTime { get; set; }

        public int CarId { get; set; }

        public decimal WrittenOffMoney { get; set; }

        public Transaction(int carId, decimal fee)
        {
            CarId = carId;
            WrittenOffMoney = fee;
            TransactionTime = DateTime.Now;
        }

    }
}
