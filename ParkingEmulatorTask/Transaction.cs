using System;
using System.IO;

namespace ParkingEmulatorTask
{
    class Transaction
    {
        private static bool isFirstTransaction = true;
        public readonly DateTime TransactionTime;
        public readonly int CarId;
        public readonly double WrittenOffMoney;

        public Transaction(int carId, double fee)
        {
            CarId = carId;
            WrittenOffMoney = fee;
            TransactionTime = DateTime.Now;
            AddTransaction();
        }

        private void AddTransaction()
        {
            if (isFirstTransaction)
            {
                using (StreamWriter writer = new StreamWriter("./Transactions.log"))
                {
                    writer.WriteLine("CarId\tWritten Off Money\tTransaction Time");
                }
                isFirstTransaction = false;
            }


            using (StreamWriter writer = new StreamWriter("./Transactions.log",true))
            {
                writer.WriteLine(CarId + "\t" + WrittenOffMoney.ToString("F") + "\t\t\t" + TransactionTime + "\n");
            }
        }
    }
}
